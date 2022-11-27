﻿namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

    public class DishesService : IDishesService
    {
        private readonly IDeletableEntityRepository<Dish> dishesRepo;

        public DishesService(IDeletableEntityRepository<Dish> dishesRepo)
        {
            this.dishesRepo = dishesRepo;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.dishesRepo.AllAsNoTracking().CountAsync();
        }
    }
}
