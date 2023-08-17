using MockQueryable.Moq;
using Moq;
using MyHotelWebsite.Data.Common.Repositories;
using MyHotelWebsite.Data.Models;
using MyHotelWebsite.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyHotelWebsite.Services.Data.Tests
{
    public class OrdersServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Order>> ordersRepo;
        private readonly Mock<IDeletableEntityRepository<DishOrder>> dishOrdersRepo;
        private readonly Mock<IDeletableEntityRepository<ShoppingCart>> shoppingCartsRepo;

        public OrdersServiceTests()
        {
            this.ordersRepo = new Mock<IDeletableEntityRepository<Order>>();
            this.dishOrdersRepo = new Mock<IDeletableEntityRepository<DishOrder>>();
            this.shoppingCartsRepo = new Mock<IDeletableEntityRepository<ShoppingCart>>();
        }

        [Fact]
        public async Task DoesOrderExistsAsyncShouldWorkCorrectly()
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

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Comment = "This is my first order",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
                new Order()
                {
                    Id = 2,
                    Comment = "This is my second order",
                    OrderStatus = OrderStatus.InProgress,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
            };
            var dishOrders = new List<DishOrder>()
            {
                new DishOrder()
                {
                    DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    OrderId = 1,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
                new DishOrder()
                {
                    DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    OrderId = 2,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
            };
            var ordersMock = orders.AsQueryable().BuildMock();
            var dishesMock = dishes.AsQueryable().BuildMock();
            var dishesOrdersMock = dishOrders.AsQueryable().BuildMock();
            this.ordersRepo.Setup(x => x.AllAsNoTracking()).Returns(ordersMock);
            this.dishOrdersRepo.Setup(x => x.AllAsNoTracking()).Returns(dishesOrdersMock);
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            Assert.True(await ordersService.DoesOrderExistsAsync(2));
            Assert.False(await ordersService.DoesOrderExistsAsync(5));
        }

        [Fact]
        public async Task GetCountAsyncShouldReturnCorrectNumber()
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

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Comment = "This is my first order",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
                new Order()
                {
                    Id = 2,
                    Comment = "This is my second order",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
            };
            var dishOrders = new List<DishOrder>()
            {
                new DishOrder()
                {
                    DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    OrderId = 1,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
                new DishOrder()
                {
                    DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    OrderId = 2,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
            };
            var ordersMock = orders.AsQueryable().BuildMock();
            var dishesMock = dishes.AsQueryable().BuildMock();
            var dishesOrdersMock = dishOrders.AsQueryable().BuildMock();
            this.ordersRepo.Setup(x => x.AllAsNoTracking()).Returns(ordersMock);
            this.dishOrdersRepo.Setup(x => x.AllAsNoTracking()).Returns(dishesOrdersMock);
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            Assert.Equal(2, await ordersService.GetCountAsync());
        }

        [Fact]
        public async Task GetCountOfMyOrdersAsyncShouldReturnCorrectNumber()
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

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Comment = "This is my first order",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
                new Order()
                {
                    Id = 2,
                    Comment = "This is my second order",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
                new Order()
                {
                    Id = 3,
                    Comment = "This is third order, not mine",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "a39b2980-37dd-4be3-a1bc-4c1399a0578e",
                },
            };
            var dishOrders = new List<DishOrder>()
            {
                new DishOrder()
                {
                    DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    OrderId = 1,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
                new DishOrder()
                {
                    DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    OrderId = 2,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
            };
            var ordersMock = orders.AsQueryable().BuildMock();
            var dishesMock = dishes.AsQueryable().BuildMock();
            var dishesOrdersMock = dishOrders.AsQueryable().BuildMock();
            this.ordersRepo.Setup(x => x.AllAsNoTracking()).Returns(ordersMock);
            this.dishOrdersRepo.Setup(x => x.AllAsNoTracking()).Returns(dishesOrdersMock);
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            Assert.Equal(2, await ordersService.GetCountOfMyOrdersAsync("2b7462c2-fcda-42b9-87f6-d1fba671afa4"));
            Assert.Equal(1, await ordersService.GetCountOfMyOrdersAsync("a39b2980-37dd-4be3-a1bc-4c1399a0578e"));
        }

        [Fact]
        public async Task GetCountOrdersByOrderStatusAsyncShouldReturnCorrectNumber()
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

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Comment = "This is my first order",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
                new Order()
                {
                    Id = 2,
                    Comment = "This is my second order",
                    OrderStatus = OrderStatus.Ready,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                },
                new Order()
                {
                    Id = 3,
                    Comment = "This is third order, not mine",
                    OrderStatus = OrderStatus.New,
                    ApplicationUserId = "a39b2980-37dd-4be3-a1bc-4c1399a0578e",
                },
            };
            var dishOrders = new List<DishOrder>()
            {
                new DishOrder()
                {
                    DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    OrderId = 1,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
                new DishOrder()
                {
                    DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    OrderId = 2,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                    DishQuantity = 2,
                },
            };
            var ordersMock = orders.AsQueryable().BuildMock();
            var dishesMock = dishes.AsQueryable().BuildMock();
            var dishesOrdersMock = dishOrders.AsQueryable().BuildMock();
            this.ordersRepo.Setup(x => x.AllAsNoTracking()).Returns(ordersMock);
            this.dishOrdersRepo.Setup(x => x.AllAsNoTracking()).Returns(dishesOrdersMock);
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            Assert.Equal(2, await ordersService.GetCountOfOrdersByOrderStatusAsync(OrderStatus.New));
            Assert.Equal(1, await ordersService.GetCountOfOrdersByOrderStatusAsync(OrderStatus.Ready));
        }
    }
}
