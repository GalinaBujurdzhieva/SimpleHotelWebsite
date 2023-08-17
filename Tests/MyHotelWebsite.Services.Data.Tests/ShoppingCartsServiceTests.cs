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
        [Fact]
        public async Task AddDishInTheShoppingCartAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestShoppingCartDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepo = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
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
            await shoppingCartsService.AddDishInTheShoppingCartAsync(shoppingCartVM);
            var shoppingCarts = await shoppingCartsRepo.AllAsNoTracking().ToListAsync();
            ShoppingCart shoppingCartDB = await shoppingCartsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == shoppingCartVM.Id);
            Assert.Equal(shoppingCartVM.Id, shoppingCartDB.Id);
            Assert.Equal(shoppingCartVM.ApplicationUserId, shoppingCartDB.ApplicationUserId);
            Assert.Equal(shoppingCartVM.Count, shoppingCartDB.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task IncreaseQuantityOfTheDishInTheShoppingCartShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestShoppingCartDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepo = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
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
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            await shoppingCartsService.IncreaseQuantityOfTheDishInTheShoppingCart(1);
            ShoppingCart shoppingCart = await shoppingCartsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.ApplicationUserId == "da19225f-939e-422f-83f8-b88cbbfb13f4" && x.DishId == "5605346a-c009-4ed4-ae79-2abe17a047b4");
            Assert.Equal(4, shoppingCart.Count);
            dbContext.Dispose();
        }
    }
}
