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
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Dishes;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
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
        public async Task DecreaseQuantityOfTheDishInTheShoppingCartShouldWorkCorrectly()
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
                ApplicationUserId = "8fc7c145-3bb7-48c2-ab28-404845766053",
                Count = 3,
                DishId = "14c77516-b7bf-418e-a13b-63df9c908f12",
                Dish = new Dish()
                {
                    Id = "14c77516-b7bf-418e-a13b-63df9c908f12",
                    Name = "Dish Test 5",
                    Price = 8.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_5.png",
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "a982c3e2-fb4b-46d5-b7e5-4e3589de400b",
                    UserName = "Test User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestUser@gmail.com",
                    FirstName = "Ivcho",
                    LastName = "Ivov",
                    PhoneNumber = "00359888333987",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            await shoppingCartsService.DecreaseQuantityOfTheDishInTheShoppingCart(1);
            ShoppingCart shoppingCart = await shoppingCartsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.ApplicationUserId == "a982c3e2-fb4b-46d5-b7e5-4e3589de400b" && x.DishId == "14c77516-b7bf-418e-a13b-63df9c908f12");
            Assert.Equal(2, shoppingCart.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task DecreaseQuantityOfTheDishInTheShoppingCartShouldWorkCorrectlyAndDeleteDishWhenCountIs1()
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
                ApplicationUserId = "8fc7c145-3bb7-48c2-ab28-404845766053",
                Count = 1,
                DishId = "14c77516-b7bf-418e-a13b-63df9c908f12",
                Dish = new Dish()
                {
                    Id = "14c77516-b7bf-418e-a13b-63df9c908f12",
                    Name = "Dish Test 5",
                    Price = 8.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_5.png",
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "a982c3e2-fb4b-46d5-b7e5-4e3589de400b",
                    UserName = "Test User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestUser@gmail.com",
                    FirstName = "Ivcho",
                    LastName = "Ivov",
                    PhoneNumber = "00359888333987",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            await shoppingCartsService.DecreaseQuantityOfTheDishInTheShoppingCart(1);
            ShoppingCart shoppingCart = await shoppingCartsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.ApplicationUserId == "a982c3e2-fb4b-46d5-b7e5-4e3589de400b" && x.DishId == "14c77516-b7bf-418e-a13b-63df9c908f12");
            Assert.Null(shoppingCart);
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetAllSingleShoppingCartsOfTheUserAsyncShouldWorkCorrect()
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
                ApplicationUserId = "94fbde06-f24a-4a87-89e9-c86bba2f9180",
                Count = 4,
                DishId = "a5c9e578-8257-4790-9a89-5ceae39c3489",
                Dish = new Dish()
                {
                    Id = "a5c9e578-8257-4790-9a89-5ceae39c3489",
                    Name = "Dish Test 11",
                    Price = 9.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 170,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_11.png",
                    DishImage = new DishImage
                    {
                        Extension = "png",
                    },
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "94fbde06-f24a-4a87-89e9-c86bba2f9180",
                    UserName = "Testing User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestingUser@gmail.com",
                    FirstName = "Deni",
                    LastName = "Valeva",
                    PhoneNumber = "00359888123456",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.AddAsync(new ShoppingCart()
            {
                Id = 2,
                ApplicationUserId = "94fbde06-f24a-4a87-89e9-c86bba2f9180",
                Count = 5,
                DishId = "2320477e-c2ec-44af-b022-b19491154bdf",
                Dish = new Dish()
                {
                    Id = "2320477e-c2ec-44af-b022-b19491154bdf",
                    Name = "Dish Test 21",
                    Price = 10.00M,
                    DishCategory = DishCategory.ColdDrinks,
                    QuantityInStock = 200,
                    DishImageUrl = "images/dishes/coldDrinks/dish_test_21.png",
                    DishImage = new DishImage
                    {
                        Extension = "png",
                    },
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            List<SingleShoppingCartViewModel> allShoppingCartsOfTheCurrentUser = await shoppingCartsService.GetAllSingleShoppingCartsOfTheUserAsync("94fbde06-f24a-4a87-89e9-c86bba2f9180");
            Assert.NotNull(allShoppingCartsOfTheCurrentUser);
            Assert.Equal(2, allShoppingCartsOfTheCurrentUser.Count());
            dbContext.Dispose();
        }

        [Fact]
        public void GetOrderTotalOfShoppingCartsOfTheUserShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            IEnumerable<SingleShoppingCartViewModel> dishesList = new List<SingleShoppingCartViewModel>()
            {
                new SingleShoppingCartViewModel()
                {
                    DishId = "643cf710-07dd-49bb-8050-198540ec8165",
                    ApplicationUserId = "8f30d51a-21ce-4be5-985a-e00f74779a94",
                    Count = 5,
                    Dish = new SingleDishViewModel()
                       {
                           Id = "643cf710-07dd-49bb-8050-198540ec8165",
                           Name = "Dish Test 11",
                           Price = 5.00M,
                           DishCategory = DishCategory.ColdDrinks,
                           QuantityInStock = 80,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_11.png",
                       },
                    ApplicationUser = new ApplicationUser()
                    {
                        Id = "8f30d51a-21ce-4be5-985a-e00f74779a94",
                        UserName = "Test User 1",
                        PasswordHash = Guid.NewGuid().ToString(),
                        Email = "TestUser1@gmail.com",
                        FirstName = "Ivcho",
                        LastName = "Ivov",
                        PhoneNumber = "00359888123987",
                        EmailConfirmed = true,
                    },
                },
                new SingleShoppingCartViewModel()
                {
                    DishId = "f624eb7c-9cfc-4c41-a1b5-363166a68379",
                    ApplicationUserId = "8f30d51a-21ce-4be5-985a-e00f74779a94",
                    Count = 3,
                    Dish = new SingleDishViewModel()
                    {
                           Id = "f624eb7c-9cfc-4c41-a1b5-363166a68379",
                           Name = "Dish Test 12",
                           Price = 4.00M,
                           DishCategory = DishCategory.ColdDrinks,
                           QuantityInStock = 50,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_12.png",
                    },
                    ApplicationUser = new ApplicationUser()
                    {
                        Id = "8f30d51a-21ce-4be5-985a-e00f74779a94",
                        UserName = "Test User 1",
                        PasswordHash = Guid.NewGuid().ToString(),
                        Email = "TestUser1@gmail.com",
                        FirstName = "Ivcho",
                        LastName = "Ivov",
                        PhoneNumber = "00359888123987",
                        EmailConfirmed = true,
                    },
                },
            };
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepoDB, dishesRepoDB);
            decimal totalSum = shoppingCartsService.GetOrderTotalOfShoppingCartsOfTheUser(dishesList);
            Assert.Equal(37, totalSum);
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
                ApplicationUserId = "8fc7c145-3bb7-48c2-ab28-404845766053",
                Count = 3,
                DishId = "14c77516-b7bf-418e-a13b-63df9c908f12",
                Dish = new Dish()
                {
                    Id = "14c77516-b7bf-418e-a13b-63df9c908f12",
                    Name = "Dish Test 5",
                    Price = 8.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_5.png",
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "a982c3e2-fb4b-46d5-b7e5-4e3589de400b",
                    UserName = "Test User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestUser@gmail.com",
                    FirstName = "Ivcho",
                    LastName = "Ivov",
                    PhoneNumber = "00359888333987",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            await shoppingCartsService.IncreaseQuantityOfTheDishInTheShoppingCart(1);
            ShoppingCart shoppingCart = await shoppingCartsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.ApplicationUserId == "a982c3e2-fb4b-46d5-b7e5-4e3589de400b" && x.DishId == "14c77516-b7bf-418e-a13b-63df9c908f12");
            Assert.Equal(4, shoppingCart.Count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task IsDishAlreadyInTheShoppingCartOfThatUserShouldWorkCorrectly()
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
                ApplicationUserId = "8fc7c145-3bb7-48c2-ab28-404845766053",
                Count = 3,
                DishId = "14c77516-b7bf-418e-a13b-63df9c908f12",
                Dish = new Dish()
                {
                    Id = "14c77516-b7bf-418e-a13b-63df9c908f12",
                    Name = "Dish Test 5",
                    Price = 8.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_5.png",
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "a982c3e2-fb4b-46d5-b7e5-4e3589de400b",
                    UserName = "Test User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestUser@gmail.com",
                    FirstName = "Ivcho",
                    LastName = "Ivov",
                    PhoneNumber = "00359888333987",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            Assert.True(await shoppingCartsService.IsDishAlreadyInTheShoppingCartOfThatUserAsync("14c77516-b7bf-418e-a13b-63df9c908f12", "a982c3e2-fb4b-46d5-b7e5-4e3589de400b"));
            dbContext.Dispose();
        }

        [Fact]
        public async Task RemoveDishFromTheShoppingCartOfThatUserShouldWorkCorrectly()
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
                ApplicationUserId = "8fc7c145-3bb7-48c2-ab28-404845766053",
                Count = 1,
                DishId = "14c77516-b7bf-418e-a13b-63df9c908f12",
                Dish = new Dish()
                {
                    Id = "14c77516-b7bf-418e-a13b-63df9c908f12",
                    Name = "Dish Test 5",
                    Price = 8.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_5.png",
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "8fc7c145-3bb7-48c2-ab28-404845766053",
                    UserName = "Test User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestUser@gmail.com",
                    FirstName = "Ivcho",
                    LastName = "Ivov",
                    PhoneNumber = "00359888333987",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            await shoppingCartsService.RemoveDishFromTheShoppingCart(1);
            Assert.Equal(0, shoppingCartsRepo.AllAsNoTracking().Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task UpdateDishInTheShoppingCartAsyncShouldWorkCorrectly()
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
                ApplicationUserId = "8fc7c145-3bb7-48c2-ab28-404845766053",
                Count = 3,
                DishId = "14c77516-b7bf-418e-a13b-63df9c908f12",
                Dish = new Dish()
                {
                    Id = "14c77516-b7bf-418e-a13b-63df9c908f12",
                    Name = "Dish Test 5",
                    Price = 8.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/alcoholDrinks/dish_test_5.png",
                },
                ApplicationUser = new ApplicationUser()
                {
                    Id = "8fc7c145-3bb7-48c2-ab28-404845766053",
                    UserName = "Test User",
                    PasswordHash = Guid.NewGuid().ToString(),
                    Email = "TestUser@gmail.com",
                    FirstName = "Ivcho",
                    LastName = "Ivov",
                    PhoneNumber = "00359888333987",
                    EmailConfirmed = true,
                },
            });
            await shoppingCartsRepo.SaveChangesAsync();
            EfDeletableEntityRepository<Dish> dishesRepo = new EfDeletableEntityRepository<Dish>(dbContext);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepo, dishesRepo);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            await shoppingCartsService.UpdateDishCountInTheShoppingCartAsync("14c77516-b7bf-418e-a13b-63df9c908f12", "8fc7c145-3bb7-48c2-ab28-404845766053", 5);
            ShoppingCart shoppingCart = await shoppingCartsRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == 1);
            Assert.Equal(8, shoppingCart.Count);
            dbContext.Dispose();
        }
    }
}
