﻿namespace MyHotelWebsite.Services.Data
{
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
        private readonly IDeletableEntityRepository<Dish> dishesRepo;

        public ShoppingCartsService(IDeletableEntityRepository<ShoppingCart> shoppingCartsRepo, IDeletableEntityRepository<Dish> dishesRepo)
        {
                this.shoppingCartsRepo = shoppingCartsRepo;
                this.dishesRepo = dishesRepo;
        }

        public async Task AddDishInTheShoppingCartAsync(SingleShoppingCartViewModel shoppingCart)
        {
            Dish currentDish = await this.dishesRepo.AllAsNoTracking().FirstOrDefaultAsync(d => d.Id == shoppingCart.DishId);
            if (currentDish != null && currentDish.QuantityInStock > 0)
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
        }

        public async Task DecreaseQuantityOfTheDishInTheShoppingCart(int shoppingCartId)
        {
            var currentShoppingCart = await this.shoppingCartsRepo.All()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == shoppingCartId);
            if (currentShoppingCart != null)
            {
                if (currentShoppingCart.Count <= 1)
                {
                    this.shoppingCartsRepo.Delete(currentShoppingCart);
                }
                else
                {
                    currentShoppingCart.Count -= 1;
                }

                await this.shoppingCartsRepo.SaveChangesAsync();
            }
        }

        public async Task<List<SingleShoppingCartViewModel>> GetAllSingleShoppingCartsOfTheUser(string applicationUserId)
        {
            var allShoppingCartsOfTheCurrentUser = await this.shoppingCartsRepo.AllAsNoTracking()
                .Include(c => c.Dish)
                .ThenInclude(d => d.DishImage)
                .Include(c => c.ApplicationUser)
                .Where(c => c.ApplicationUserId == applicationUserId)
                .OrderBy(c => c.Dish.Name)
                .To<SingleShoppingCartViewModel>()
                .ToListAsync();
            return allShoppingCartsOfTheCurrentUser;
        }

        public decimal GetOrderTotalOfShoppingCartsOfTheUser(IEnumerable<SingleShoppingCartViewModel> shoppingCartsList)
        {
            decimal orderTotal = 0m;

            foreach (var shoppingCart in shoppingCartsList)
            {
                orderTotal += shoppingCart.Dish.Price * shoppingCart.Count;
            }

            return orderTotal;
        }

        public async Task IncreaseQuantityOfTheDishInTheShoppingCart(int shoppingCartId)
        {
            var currentShoppingCart = await this.shoppingCartsRepo.All()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == shoppingCartId);
            if (currentShoppingCart != null)
            {
                currentShoppingCart.Count += 1;
                await this.shoppingCartsRepo.SaveChangesAsync();
            }
        }

        public async Task<bool> IsDishAlreadyInTheShoppingCartOfThatUserAsync(string dishId, string applicationUserId)
        {
            return await this.shoppingCartsRepo.AllAsNoTracking()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .AnyAsync(c => c.DishId == dishId && c.ApplicationUserId == applicationUserId);
        }

        public async Task RemoveDishFromTheShoppingCart(int shoppingCartId)
        {
            var currentShoppingCart = await this.shoppingCartsRepo.All()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == shoppingCartId);
            if (currentShoppingCart != null)
            {
                this.shoppingCartsRepo.Delete(currentShoppingCart);
                await this.shoppingCartsRepo.SaveChangesAsync();
            }
        }

        public async Task UpdateDishCountInTheShoppingCartAsync(string dishId, string applicationUserId, int count)
        {
            var currentShoppingCart = await this.shoppingCartsRepo.All()
                .Include(c => c.Dish)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.DishId == dishId && c.ApplicationUserId == applicationUserId);

            if (currentShoppingCart != null)
            {
                currentShoppingCart.Count += count;
                await this.shoppingCartsRepo.SaveChangesAsync();
            }
        }
    }
}
