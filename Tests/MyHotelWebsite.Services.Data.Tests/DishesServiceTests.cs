namespace MyHotelWebsite.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;
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
    }
}
