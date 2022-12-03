namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class DishesService : IDishesService
    {
        private readonly IDeletableEntityRepository<Dish> dishesRepo;

        public DishesService(IDeletableEntityRepository<Dish> dishesRepo)
        {
            this.dishesRepo = dishesRepo;
        }

        public async Task<IEnumerable<T>> GetAlcoholDrinksAsync<T>(int page, int itemsPerPage = 4)
        {
            var alcoholDrinks = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.DishCategory == DishCategory.AlcoholDrinks)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return alcoholDrinks;
        }

        public async Task<IEnumerable<T>> GetAppetizersAsync<T>(int page, int itemsPerPage = 4)
        {
            var appetizers = await this.dishesRepo.AllAsNoTracking()
               .OrderBy(x => Guid.NewGuid())
               .Where(x => x.DishCategory == DishCategory.Appetizers)
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage).To<T>().ToListAsync();
            return appetizers;
        }

        public async Task<IEnumerable<T>> GetColdDrinksAsync<T>(int page, int itemsPerPage = 4)
        {
            var coldDrinks = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.DishCategory == DishCategory.ColdDrinks)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return coldDrinks;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.dishesRepo.AllAsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<T>> GetDessertsAsync<T>(int page, int itemsPerPage = 4)
        {
            var desserts = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.DishCategory == DishCategory.Desserts)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return desserts;
        }

        public async Task<IEnumerable<T>> GetGourmetsAsync<T>(int page, int itemsPerPage = 4)
        {
            var gourmets = await this.dishesRepo.AllAsNoTracking()
                 .OrderBy(x => Guid.NewGuid())
                 .Where(x => x.DishCategory == DishCategory.Gourmets)
                 .Skip((page - 1) * itemsPerPage)
                 .Take(itemsPerPage).To<T>().ToListAsync();
            return gourmets;
        }

        public async Task<IEnumerable<T>> GetHotDrinksAsync<T>(int page, int itemsPerPage = 4)
        {
            var hotDrinks = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.DishCategory == DishCategory.HotDrinks)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return hotDrinks;
        }

        public async Task<IEnumerable<T>> GetMainCoursesAsync<T>(int page, int itemsPerPage = 4)
        {
            var mainCourses = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.DishCategory == DishCategory.MainCourses)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return mainCourses;
        }

        public async Task<IEnumerable<T>> GetRandomDishesAsync<T>(int page, int itemsPerPage = 4)
        {
            var randomDishes = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return randomDishes;
        }

        public async Task<IEnumerable<T>> GetSaladsAsync<T>(int page, int itemsPerPage = 4)
        {
            var salads = await this.dishesRepo.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.DishCategory == DishCategory.Salads)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return salads;
        }
    }
}
