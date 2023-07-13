namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;

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
            Directory.CreateDirectory($"{imagePath}/dishes/{dish.DishCategory}");
            var dishImageExtension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var dishImage = new DishImage
            {
                Extension = dishImageExtension,
                ApplicationUserId = applicationUserId,
            };

            dish.DishImage = dishImage;
            dish.DishImageId = dishImage.Id;
            dish.DishImageUrl = $"images/dishes/{dish.DishCategory}/{dishImage.Id}.{dishImageExtension}";
            var physicalPath = $"{imagePath}/dishes/{dish.DishCategory}/{dishImage.Id}.{dishImageExtension}";
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
                .Include(d => d.DishImage)
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();
            return currentDish;
        }

        public async Task<bool> DoesDishExistsAsync(string id)
        {
            return await this.dishesRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task EditDishAsync(EditDishViewModel model, string id, string applicationUserId, string imagePath, IFormFile? file)
        {
            var currentDish = await this.dishesRepo.All().Include(d => d.DishImage).FirstOrDefaultAsync(x => x.Id == id);
            currentDish.Name = model.Name;
            currentDish.Price = model.Price;
            currentDish.DishCategory = model.DishCategory;
            currentDish.QuantityInStock = model.QuantityInStock;

            var currentDishImage = await this.dishImagesRepo.All().FirstOrDefaultAsync(x => x.DishId == currentDish.Id);
            currentDish.ApplicationUserId = applicationUserId;

            if (file != null)
            {
                var oldDishImagePhysicalPath = $"{imagePath}/dishes/{currentDish.DishCategory}/{currentDishImage.Id}.{currentDishImage.Extension}";
                if (currentDishImage != null)
                {
                    this.dishImagesRepo.HardDelete(currentDishImage);
                }

                if (File.Exists(oldDishImagePhysicalPath))
                {
                    File.Delete(oldDishImagePhysicalPath);
                }

                Directory.CreateDirectory($"{imagePath}/dishes/{currentDish.DishCategory}");
                var dishImageExtension = Path.GetExtension(file.FileName).TrimStart('.');

                var dishImage = new DishImage
                {
                    Extension = dishImageExtension,
                    ApplicationUserId = applicationUserId,
                };

                currentDish.DishImage = dishImage;
                currentDish.DishImageId = dishImage.Id;
                currentDish.DishImageUrl = $"images/dishes/{currentDish.DishCategory}/{dishImage.Id}.{dishImageExtension}";
                var physicalPath = $"{imagePath}/dishes/{currentDish.DishCategory}/{dishImage.Id}.{dishImageExtension}";
                using FileStream fileStream = new FileStream(physicalPath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            else
            {
                currentDish.DishImage = currentDishImage;
                currentDish.DishImageId = currentDishImage.Id;
                currentDish.DishImageUrl = currentDish.DishImageUrl;
            }

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

        public async Task<int> GetCountOfDishesByCategoryAsync(bool? isInStock = null, bool isReady = false, DishCategory dishCategory = 0, DishSorting sorting = DishSorting.Name)
        {
            var searchDishesList = this.dishesRepo.AllAsNoTracking().AsQueryable();

            if (dishCategory != 0)
            {
                searchDishesList = searchDishesList
                    .Where(x => x.DishCategory == dishCategory);
            }

            if (isReady)
            {
                searchDishesList = searchDishesList.Where(x => x.IsReady);
            }

            if (isInStock == true)
            {
                searchDishesList = searchDishesList.Where(x => x.QuantityInStock > 0);
            }

            if (isInStock == false)
            {
                searchDishesList = searchDishesList.Where(x => x.QuantityInStock < 1);
            }

            searchDishesList = sorting switch
            {
                DishSorting.Price => searchDishesList.OrderBy(x => x.Price),
                DishSorting.Newest => searchDishesList.OrderByDescending(x => x.CreatedOn),
                _ => searchDishesList.OrderByDescending(x => x.Name),
            };

            return await searchDishesList.CountAsync();
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

        public async Task<IEnumerable<T>> GetDishesByDishCategoryAsync<T>(int page, DishCategory dishCategory, DishSorting sorting, bool? isInStock = null, bool isReady = false, int itemsPerPage = 4)
        {
            var dishesByCategory = this.dishesRepo.AllAsNoTracking().AsQueryable();

            if (dishCategory != 0)
            {
                dishesByCategory = dishesByCategory
                    .Where(x => x.DishCategory == dishCategory);
            }

            if (isReady)
            {
                dishesByCategory = dishesByCategory.Where(x => x.IsReady);
            }

            if (isInStock == true)
            {
                dishesByCategory = dishesByCategory.Where(x => x.QuantityInStock > 0);
            }

            if (isInStock == false)
            {
                dishesByCategory = dishesByCategory.Where(x => x.QuantityInStock < 1);
            }

            dishesByCategory = sorting switch
            {
                DishSorting.Price => dishesByCategory.OrderBy(x => x.Price),
                DishSorting.Newest => dishesByCategory.OrderByDescending(x => x.CreatedOn),
                _ => dishesByCategory.OrderByDescending(x => x.Name),
            };

            return await dishesByCategory.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToListAsync();
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
