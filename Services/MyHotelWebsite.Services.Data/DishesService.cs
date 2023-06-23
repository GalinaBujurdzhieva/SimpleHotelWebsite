namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;

    public class DishesService : IDishesService
    {
        private readonly IDeletableEntityRepository<Dish> dishesRepo;
        private readonly IDeletableEntityRepository<DishImage> dishImagesRepo;

        public DishesService(IDeletableEntityRepository<Dish> dishesRepo, IDeletableEntityRepository<DishImage> dishImagesRepo)
        {
            this.dishesRepo = dishesRepo;
            this.dishImagesRepo = dishImagesRepo;
        }

        public async Task AddDishAsync(CreateDishViewModel model, string applicationUserId, string imagePath)
        {
            var dish = new Dish
            {
                Name = model.Name,
                Price = model.Price,
                DishCategory = model.DishCategory,
                QuantityInStock = model.QuantityInStock,
                ApplicationUserId = applicationUserId,
            };
            Directory.CreateDirectory($"{imagePath}/dishes/addedLater");
            var dishImageExtension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var dishImage = new DishImage
            {
                Extension = dishImageExtension,
                ApplicationUserId = applicationUserId,
            };

            dish.DishImage = dishImage;
            dish.DishImageId = dishImage.Id;
            dish.DishImageUrl = $"images/dishes/addedLater/{dishImage.Id}.{dishImageExtension}";
            var physicalPath = $"{imagePath}/dishes/addedLater/{dishImage.Id}.{dishImageExtension}";
            using FileStream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this.dishesRepo.AddAsync(dish);
            await this.dishesRepo.SaveChangesAsync();
        }

        public async Task DeleteDishAsync(string id)
        {
            var currentDish = await this.dishesRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            this.dishesRepo.Delete(currentDish);
            await this.dishesRepo.SaveChangesAsync();
        }

        public async Task<T> DishDetailsByIdAsync<T>(string id)
        {
            var currentDish = await this.dishesRepo.AllAsNoTracking()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();
            return currentDish;
        }

        public async Task<bool> DoesDishExistsAsync(string id)
        {
            return await this.dishesRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task EditDishAsync(EditDishViewModel model, string id, string applicationUserId, string imagePath)
        {
            var currentDish = await this.dishesRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            currentDish.Name = model.Name;
            currentDish.Price = model.Price;
            currentDish.DishCategory = model.DishCategory;
            currentDish.QuantityInStock = model.QuantityInStock;

            var currentDishImage = await this.dishImagesRepo.All().FirstOrDefaultAsync(x => x.DishId == currentDish.Id);
            if (currentDishImage != null)
            {
                this.dishImagesRepo.HardDelete(currentDishImage);
            }

            currentDish.ApplicationUserId = applicationUserId;

            Directory.CreateDirectory($"{imagePath}/dishes/addedLater");
            var dishImageExtension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var dishImage = new DishImage
            {
                Extension = dishImageExtension,
                ApplicationUserId = applicationUserId,
            };

            currentDish.DishImage = dishImage;
            currentDish.DishImageId = dishImage.Id;
            currentDish.DishImageUrl = $"images/dishes/addedLater/{dishImage.Id}.{dishImageExtension}";
            var physicalPath = $"{imagePath}/dishes/addedLater/{dishImage.Id}.{dishImageExtension}";
            using FileStream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this.dishesRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllDishesAsync<T>(int page, int itemsPerPage = 4)
        {
            var allDishes = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => x.DishCategory)
                .ThenBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return allDishes;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.dishesRepo.AllAsNoTracking().CountAsync();
        }

        public async Task<int> GetCountOfDishesByCategoryAsync(DishCategory dishCategory)
        {
            return await this.dishesRepo.AllAsNoTracking()
                .Where(x => x.DishCategory == dishCategory)
                .CountAsync();
        }

        public async Task<int> GetCountOfDishesByNameAndCategoryAsync(string name = null, DishCategory dishCategory = 0)
        {
            var searchDishesList = this.dishesRepo.All().AsQueryable();
            if (dishCategory != 0)
            {
                searchDishesList = searchDishesList
                    .Where(x => x.DishCategory == dishCategory);
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = $"%{name.ToLower()}%";
                searchDishesList = searchDishesList
                    .Where(x => EF.Functions.Like(x.Name.ToLower(), name));
            }

            return await searchDishesList.CountAsync();
        }

        public async Task<IEnumerable<T>> GetDishesByDishCategoryAsync<T>(int page, DishCategory dishCategory, int itemsPerPage = 4)
        {
            if (!string.IsNullOrEmpty(dishCategory.ToString()))
            {
                var dishesByCategory = await this.dishesRepo.AllAsNoTracking()
                    .OrderBy(x => x.Name)
                    .Where(x => x.DishCategory == dishCategory)
                    .OrderBy(x => x.Name).Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage).To<T>().ToListAsync();
                return dishesByCategory;
            }

            return null;
        }

        public async Task<IEnumerable<T>> SearchDishesByNameAndCategoryAsync<T>(int page, string name = null, DishCategory dishCategory = 0, int itemsPerPage = 4)
        {
            var searchDishesList = this.dishesRepo.All().AsQueryable();
            if (dishCategory != 0)
            {
                searchDishesList = searchDishesList
                    .Where(x => x.DishCategory == dishCategory);
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = $"%{name.ToLower()}%";
                searchDishesList = searchDishesList
                    .Where(x => EF.Functions.Like(x.Name.ToLower(), name));
            }

            return await searchDishesList
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
        }
    }
}
