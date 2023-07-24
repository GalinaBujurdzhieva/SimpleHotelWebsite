﻿namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartsRepo;

        public ShoppingCartsService(IDeletableEntityRepository<ShoppingCart> shoppingCartsRepo)
        {
                this.shoppingCartsRepo = shoppingCartsRepo;
        }

        public async Task AddDishInTheShoppingCartAsync(SingleShoppingCartViewModel shoppingCart)
        {
            ShoppingCart newShoppingCart = new ShoppingCart
            {
                DishId = shoppingCart.DishId,
                ApplicationUserId = shoppingCart.ApplicationUserId,
                Count = shoppingCart.Count,
            };
            await this.shoppingCartsRepo.AddAsync(newShoppingCart);
            await this.shoppingCartsRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllSingleShoppingCartsOfTheUser<T>(string applicationUserId)
        {
            var allShoppingCartsOfTheCurrentUser = await this.shoppingCartsRepo.AllAsNoTracking()
                .Include(c => c.Dish)
                .ThenInclude(d => d.DishImage)
                .Include(c => c.ApplicationUser)
                .Where(c => c.ApplicationUserId == applicationUserId /*&& c.CreatedOn.AddMinutes(15).CompareTo(DateTime.UtcNow) >= 0*/)
                .OrderBy(c => c.Dish.Name)
                .To<SingleShoppingCartViewModel>()
                .ToListAsync();
            return (IEnumerable<T>)allShoppingCartsOfTheCurrentUser;
        }

        public async Task<bool> IsDishAlreadyInTheShoppingCartOfThatUserAsync(string dishId, string applicationUserId)
        {
            return await this.shoppingCartsRepo.AllAsNoTracking()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .AnyAsync(c => c.DishId == dishId && c.ApplicationUserId == applicationUserId && c.CreatedOn.AddMinutes(15).CompareTo(DateTime.UtcNow) >= 0);
        }

        public async Task UpdateDishCountInTheShoppingCartAsync(string dishId, string applicationUserId, int count)
        {
            var currentShoppingCart = await this.shoppingCartsRepo.All()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.DishId == dishId && c.ApplicationUserId == applicationUserId && c.CreatedOn.AddMinutes(15).CompareTo(DateTime.UtcNow) >= 0);

            if (currentShoppingCart != null)
            {
                currentShoppingCart.Count += count;
                await this.shoppingCartsRepo.SaveChangesAsync();
            }
        }
    }
}
