namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Castle.Core.Resource;
    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Dishes;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;
    using Xunit;

    public class ShoppingCartsServiceTests
    {
        public ApplicationDbContext GetDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestShoppingCartDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            return dbContext;
        }

        public async Task<ShoppingCartsService> GetShoppingCartsService()
        {
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepo = new EfDeletableEntityRepository<ShoppingCart>(this.GetDbContext());
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(this.GetDbContext());
            await dishesRepo.AddAsync(new Dish()
            {
                Id = "5605346a-c009-4ed4-ae79-2abe17a047b4",
                Name = "Dish Test 4",
                Price = 5.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                IsReady = true,
                QuantityInStock = 200,
                DishImageUrl = "images/dishes/alcoholDrinks/dish_test_4.png",
            });
            await dishesRepo.SaveChangesAsync();
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            return shoppingCartsService;
        }

        [Fact]
        public async Task AddDishInTheShoppingCartAsyncShouldWorkCorrectly()
        {
            SingleShoppingCartViewModel shoppingCartVM = new SingleShoppingCartViewModel()
            {
                Id = 1,
                ApplicationUserId = "da19225f-939e-422f-83f8-b88cbbfb13f4",
                Count = 3,
                Dish = new SingleDishViewModel()
                {
                    Id = "5605346a-c009-4ed4-ae79-2abe17a047b4",
                    Name = "Dish Test 4",
                    Price = 5.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    IsReady = true,
                    QuantityInStock = 200,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_4.png",
                },
                DishId = "5605346a-c009-4ed4-ae79-2abe17a047b4",
            };
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            ShoppingCartsService shoppingCartsService = await this.GetShoppingCartsService();
            await shoppingCartsService.AddDishInTheShoppingCartAsync(shoppingCartVM);
            var shoppingCarts = await this.GetDbContext().ShoppingCarts.ToListAsync();
            ShoppingCart shoppingCartDB = await this.GetDbContext().ShoppingCarts.Where(x => x.Id == shoppingCartVM.Id).FirstOrDefaultAsync();
            Assert.Equal(shoppingCartVM.Id, shoppingCartDB.Id);
            Assert.Equal(shoppingCartVM.ApplicationUserId, shoppingCartDB.ApplicationUserId);
            Assert.Equal(shoppingCartVM.Count, shoppingCartDB.Count);
        }

        [Fact]
        public async Task IncreaseQuantityOfTheDishInTheShoppingCartShouldWorkCorrectly()
        {
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepo = new EfDeletableEntityRepository<ShoppingCart>(this.GetDbContext());
            await shoppingCartsRepo.AddAsync(new ShoppingCart()
            {
                Id = 1,
                ApplicationUserId = "da19225f-939e-422f-83f8-b88cbbfb13f4",
                Count = 3,
                Dish = new Dish()
                {
                    Id = "5605346a-c009-4ed4-ae79-2abe17a047b4",
                    Name = "Dish Test 4",
                    Price = 5.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    IsReady = true,
                    QuantityInStock = 200,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_4.png",
                },
                DishId = "5605346a-c009-4ed4-ae79-2abe17a047b4",
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(this.GetDbContext());
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            await shoppingCartsService.IncreaseQuantityOfTheDishInTheShoppingCart(1);
            ShoppingCart shoppingCart = await this.GetDbContext().ShoppingCarts.Where(x => x.Id == 1).FirstOrDefaultAsync();
            Assert.Equal(4, shoppingCart.Count);
        }
    }
}
