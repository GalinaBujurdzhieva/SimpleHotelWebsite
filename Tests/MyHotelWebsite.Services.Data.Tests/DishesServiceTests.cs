namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;
    using MyHotelWebsite.Web.ViewModels.Dishes;
    using Xunit;

    public class DishesServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Dish>> dishesRepo;
        private readonly Mock<IDeletableEntityRepository<DishImage>> dishesImagesRepo;

        public DishesServiceTests()
        {
            this.dishesRepo = new Mock<IDeletableEntityRepository<Dish>>();
            this.dishesImagesRepo = new Mock<IDeletableEntityRepository<DishImage>>();
        }

        [Fact]
        public async Task DishDetailsByIdAsyncShouldWorkCorrectly()
        {
            var dishes = new List<Dish>()
            {
                new Dish
                {
                Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                Name = "Dish Test 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
                new Dish
                {
                Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                Name = "Dish Test 2",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
            };
            var mock = dishes.AsQueryable().BuildMock();
            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            var currentDish = await dishesService.DishDetailsByIdAsync<SingleDishViewModel>("5ea80afe-706a-4628-8ebb-ef7523da6e8f");
            var notExistingDish = await dishesService.DishDetailsByIdAsync<SingleDishViewModel>("7b7a6698-24f9-4fea-9752-e6fbef1332f7");
            Assert.NotNull(currentDish);
            Assert.Null(notExistingDish);
            Assert.Equal("5ea80afe-706a-4628-8ebb-ef7523da6e8f", currentDish.Id);
            Assert.Equal("Dish Test 1", currentDish.Name);
            Assert.Equal(2.00M, currentDish.Price);
            Assert.Equal(50, currentDish.QuantityInStock);
            Assert.Equal(DishCategory.HotDrinks, currentDish.DishCategory);
        }

        [Fact]
        public async Task DishQuantityInStockAsyncShouldWorkCorrectly()
        {
            var dishes = new List<Dish>()
            {
                new Dish
                {
                Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                Name = "Dish Test 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
                new Dish
                {
                Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                Name = "Dish Test 2",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
            };
            var mock = dishes.AsQueryable().BuildMock();
            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            var currentDishQuantity = await dishesService.DishQuantityInStockAsync("5ea80afe-706a-4628-8ebb-ef7523da6e8f");
            Assert.Equal(50, currentDishQuantity);
        }

        [Fact]
        public async Task DoesDishExistsAsyncShouldWorkCorrectly()
        {
            var dishes = new List<Dish>()
            {
                new Dish
                {
                Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                Name = "Dish Test 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
                new Dish
                {
                Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                Name = "Dish Test 2",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
            };
            var mock = dishes.AsQueryable().BuildMock();
            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            Assert.True(await dishesService.DoesDishExistsAsync("45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd"));
            Assert.False(await dishesService.DoesDishExistsAsync("9992a9c8-c7fd-4a33-9c25-fb9e9cd8acfd"));
        }

        [Fact]
        public async Task GetAllDishesAsyncShouldReturnAllDishes()
        {
            var dishes = new List<Dish>()
            {
                new Dish
                {
                Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                Name = "Dish Test 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
                new Dish
                {
                Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                Name = "Dish Test 2",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
                new Dish
                {
                Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                Name = "Dish Test 3",
                Price = 4.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 150,
                DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                },
                new Dish
                {
                Id = "64fe9ad9-90ff-4c81-b2e8-46ba166a4b72",
                Name = "Dish Test 4",
                Price = 5.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 200,
                DishImageUrl = "images/dishes/alcoholDrinks/dish_test_4.png",
                },
            };
            var mock = dishes.AsQueryable().BuildMock();
            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            IEnumerable<SingleDishViewModel> allDishesFirstPage = await dishesService.GetAllDishesAsync<SingleDishViewModel>(1, 2);
            IEnumerable<SingleDishViewModel> allDishesSecondPage = await dishesService.GetAllDishesAsync<SingleDishViewModel>(2, 2);
            Assert.NotNull(allDishesFirstPage);
            Assert.NotNull(allDishesSecondPage);
            Assert.Equal(2, allDishesFirstPage.Count());
            Assert.Equal("21a80b4d-2cab-4e5f-9d95-0ef209bb7e02", allDishesSecondPage.First().Id);
        }

        [Fact]
        public async Task GetCountAsyncShouldReturnCorrectNumber()
        {
            var dishes = new List<Dish>()
            {
                new Dish(),
                new Dish(),
                new Dish(),
                new Dish(),
                new Dish(),
            };
            var mock = dishes.AsQueryable().BuildMock();

            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            Assert.Equal(5, await dishesService.GetCountAsync());
        }

        [Fact]
        public async Task GetCountOfDishesByCategoryAsyncShouldReturnCorrectNumber()
        {
            var dishes = new List<Dish>()
            {
                new Dish
                {
                Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                Name = "Dish Test 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
                new Dish
                {
                Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                Name = "Dish Test 2",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
                new Dish
                {
                Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                Name = "Dish Test 3",
                Price = 4.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 150,
                DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                },
                new Dish
                {
                Id = "64fe9ad9-90ff-4c81-b2e8-46ba166a4b72",
                Name = "Dish Test 4",
                Price = 5.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                IsReady = true,
                QuantityInStock = 200,
                DishImageUrl = "images/dishes/alcoholDrinks/dish_test_4.png",
                },
            };
            var mock = dishes.AsQueryable().BuildMock();
            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            int countOfHotDrinksSortedByPrice = await dishesService.GetCountOfDishesByCategoryAsync(null, false, DishCategory.HotDrinks, DishSorting.Price);
            int countOfAvailableReadyAlcoholDrinksSortedByNewest = await dishesService.GetCountOfDishesByCategoryAsync(true, true, DishCategory.AlcoholDrinks, DishSorting.Newest);
            int countOfNotAvailableDishesSortedByName = await dishesService.GetCountOfDishesByCategoryAsync(false, false, DishCategory.Salads, DishSorting.Name);
            Assert.Equal(2, countOfHotDrinksSortedByPrice);
            Assert.Equal(1, countOfAvailableReadyAlcoholDrinksSortedByNewest);
            Assert.Equal(0, countOfNotAvailableDishesSortedByName);
        }

        [Fact]
        public async Task SearchDishesByCategoryAsyncShouldWorkCorrect()
        {
            var dishes = new List<Dish>()
            {
                new Dish
                {
                Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                Name = "Dish Test 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
                new Dish
                {
                Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                Name = "Dish Test 2",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 100,
                DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
                new Dish
                {
                Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                Name = "Dish Test 3",
                Price = 4.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 150,
                DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                },
                new Dish
                {
                Id = "64fe9ad9-90ff-4c81-b2e8-46ba166a4b72",
                Name = "Dish Test 4",
                Price = 5.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                IsReady = true,
                QuantityInStock = 200,
                DishImageUrl = "images/dishes/alcoholDrinks/dish_test_4.png",
                },
            };
            var mock = dishes.AsQueryable().BuildMock();
            this.dishesRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var dishesService = new DishesService(this.dishesRepo.Object, this.dishesImagesRepo.Object);
            IEnumerable<SingleDishViewModel> searchingListWithHotDrinksSortedByPrice = await dishesService.GetDishesByDishCategoryAsync<SingleDishViewModel>(1, DishCategory.HotDrinks, DishSorting.Price);
            IEnumerable<SingleDishViewModel> searchingListWithAvailableReadyAlcoholDrinksSortedByNewest = await dishesService.GetDishesByDishCategoryAsync<SingleDishViewModel>(1, DishCategory.AlcoholDrinks, DishSorting.Newest, true, true);
            IEnumerable<SingleDishViewModel> searchingListWithNotAvailableDishesSortedByName = await dishesService.GetDishesByDishCategoryAsync<SingleDishViewModel>(1, DishCategory.ColdDrinks, DishSorting.Name, false);
            Assert.NotNull(searchingListWithHotDrinksSortedByPrice);
            Assert.Equal(2, searchingListWithHotDrinksSortedByPrice.Count());

            Assert.NotNull(searchingListWithAvailableReadyAlcoholDrinksSortedByNewest);
            Assert.Single(searchingListWithAvailableReadyAlcoholDrinksSortedByNewest);

            Assert.Empty(searchingListWithNotAvailableDishesSortedByName);
        }

        // TESTS WITH IN-MEMORY DB
        [Fact]
        public async Task AddDishAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            var bytes1 = Encoding.UTF8.GetBytes("This is the first dummy file");
            var bytes2 = Encoding.UTF8.GetBytes("This is the second dummy file");
            IFormFile file1 = new FormFile(new MemoryStream(bytes1), 0, bytes1.Length, "Data", "dummy.txt");
            IFormFile file2 = new FormFile(new MemoryStream(bytes2), 0, bytes2.Length, "Data", "dummy.txt");
            CreateDishViewModel model1 = new CreateDishViewModel()
            {
                Name = "Test Dish 1",
                Price = 5,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 5,
                Image = file1,
            };
            CreateDishViewModel model2 = new CreateDishViewModel()
            {
                Name = "Test Dish 2",
                Price = 10,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 10,
                Image = file2,
            };
            await dishesService.AddDishAsync(model1, "55eddfb6-4d2d-492e-99c8-1c75ca59d673", "dummyImagePath1");
            await dishesService.AddDishAsync(model2, "b65d8626-ae18-4116-ab72-bd99cb99247c", "dummyImagePath2");
            int count = await dishesService.GetCountAsync();
            Assert.Equal(2, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task DeleteDishAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish()
                {
                    Id = "f116f2c2-92c5-4f56-aca0-209e21d30ff8",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish()
                {
                    Id = "c66ff836-699f-4b5b-92b2-ae27dd7ca9af",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                });
            await dishesRepoDB.SaveChangesAsync();
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            await dishesService.DeleteDishAsync("c66ff836-699f-4b5b-92b2-ae27dd7ca9af");
            int count = await dishesService.GetCountAsync();
            Assert.Equal(1, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task EditDishAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                    DishImageId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    DishImage = new DishImage
                    {
                        Extension = "png",
                    },
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                    DishImage = new DishImage
                    {
                        DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                        Extension = "jpg",
                    },
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                    Name = "Dish Test 3",
                    Price = 4.00M,
                    DishCategory = DishCategory.ColdDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                    DishImage = new DishImage
                    {
                        DishId = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                        Extension = "png",
                    },
                });
            await dishesRepoDB.SaveChangesAsync();
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var bytes2 = Encoding.UTF8.GetBytes("This is the second dummy file");
            IFormFile file2 = new FormFile(new MemoryStream(bytes2), 0, bytes2.Length, "Data", "dummy.txt");
            EditDishViewModel model = new EditDishViewModel
            {
                Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                Name = "Dish Test 3 - EDITED",
                Price = 10.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 100,
            };
            await dishesService.EditDishAsync(model, "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02", "b65d8626-ae18-4116-ab72-bd99cb99247c", "images", null);
            var editedDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02");
            Assert.Equal("Dish Test 3 - EDITED", editedDish.Name);
            Assert.Equal(10, editedDish.Price);
            Assert.Equal(DishCategory.Salads, editedDish.DishCategory);
            Assert.Equal(100, editedDish.QuantityInStock);
            Assert.Equal("b65d8626-ae18-4116-ab72-bd99cb99247c", editedDish.ApplicationUserId);
            Assert.Equal("images/dishes/coldDrinks/dish_test_3.png", editedDish.DishImageUrl);
            dbContext.Dispose();
        }

        [Fact]
        public async Task EditDishAsyncShouldWorkCorrectlyWithNewFile()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                    DishImage = new DishImage
                    {
                        DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                        Extension = "png",
                    },
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                    DishImage = new DishImage
                    {
                        DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                        Extension = "jpg",
                    },
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                    Name = "Dish Test 3",
                    Price = 4.00M,
                    DishCategory = DishCategory.ColdDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                    DishImage = new DishImage
                    {
                        DishId = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                        Extension = "png",
                    },
                });
            await dishesRepoDB.SaveChangesAsync();
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var bytes2 = Encoding.UTF8.GetBytes("This is the second dummy file");
            IFormFile file2 = new FormFile(new MemoryStream(bytes2), 0, bytes2.Length, "Data", "dummy.txt");
            EditDishViewModel model = new EditDishViewModel
            {
                Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                Name = "Dish Test 3 - EDITED",
                Price = 10.00M,
                DishCategory = DishCategory.Salads,
                QuantityInStock = 100,
            };
            await dishesService.EditDishAsync(model, "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02", "b65d8626-ae18-4116-ab72-bd99cb99247c", "images", file2);
            var editedDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02");
            Assert.Equal("Dish Test 3 - EDITED", editedDish.Name);
            Assert.Equal(10, editedDish.Price);
            Assert.Equal(DishCategory.Salads, editedDish.DishCategory);
            Assert.Equal(100, editedDish.QuantityInStock);
            Assert.Contains("images/dishes/Salads/", editedDish.DishImageUrl);
            Assert.Equal("b65d8626-ae18-4116-ab72-bd99cb99247c", editedDish.ApplicationUserId);
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetCountOfDishesByCategoryAndNameAsyncShouldReturnCorrectNumber()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                    Name = "Dish Test 3",
                    Price = 4.00M,
                    DishCategory = DishCategory.ColdDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                });
            await dishesRepoDB.SaveChangesAsync();
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            int countOfHotDrinksByDishCategory = await dishesService.GetCountOfDishesByNameAndCategoryAsync(null, DishCategory.HotDrinks);
            int countOfDrinksWhenSearchingByName = await dishesService.GetCountOfDishesByNameAndCategoryAsync("dish");
            Assert.Equal(2, countOfHotDrinksByDishCategory);
            Assert.Equal(3, countOfDrinksWhenSearchingByName);
            dbContext.Dispose();
        }

        [Fact]
        public async Task PrepareDishShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish()
                {
                    Id = "77536d06-c6aa-4a41-b1d4-9bcdbfc0b6f7",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish()
                {
                    Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                    IsReady = true,
                });
            await dishesRepoDB.SaveChangesAsync();
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            await dishesService.PrepareDishAsync("77536d06-c6aa-4a41-b1d4-9bcdbfc0b6f7");
            Dish preparedDish = dishesRepoDB.AllAsNoTracking().FirstOrDefault(x => x.Id == "77536d06-c6aa-4a41-b1d4-9bcdbfc0b6f7");
            await Assert.ThrowsAsync<Exception>(() => dishesService.PrepareDishAsync("45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd"));
            await Assert.ThrowsAsync<Exception>(() => dishesService.PrepareDishAsync("2b7462c2-fcda-42b9-87f6-d1fba671afa4"));
            Assert.True(preparedDish != null);
            Assert.True(preparedDish.IsReady);
            dbContext.Dispose();
        }

        [Fact]
        public async Task SearchDishesByCategoryAndNameShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "21a80b4d-2cab-4e5f-9d95-0ef209bb7e02",
                    Name = "Dish Test 3",
                    Price = 4.00M,
                    DishCategory = DishCategory.ColdDrinks,
                    QuantityInStock = 150,
                    DishImageUrl = "images/dishes/coldDrinks/dish_test_3.png",
                });
            await dishesRepoDB.SaveChangesAsync();
            DishesService dishesService = new DishesService(dishesRepoDB, dishImagesRepoDB);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            IEnumerable<SingleDishViewModel> searchingListDishesSortedByDishCategory = await dishesService.SearchDishesByNameAndCategoryAsync<SingleDishViewModel>(1, null, DishCategory.HotDrinks);
            IEnumerable<SingleDishViewModel> searchingDishesByName = await dishesService.SearchDishesByNameAndCategoryAsync<SingleDishViewModel>(1, "dish");
            Assert.NotNull(searchingListDishesSortedByDishCategory);
            Assert.Equal(2, searchingListDishesSortedByDishCategory.Count());
            Assert.NotNull(searchingDishesByName);
            Assert.Equal(3, searchingDishesByName.Count());
            dbContext.Dispose();
        }
    }
}
