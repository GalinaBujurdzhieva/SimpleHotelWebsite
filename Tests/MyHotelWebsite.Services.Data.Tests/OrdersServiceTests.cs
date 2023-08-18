namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Orders;
    using MyHotelWebsite.Web.ViewModels.Dishes;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;
    using Xunit;

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
        public async Task GetCountOfOrdersByOrderStatusAsyncShouldReturnCorrectNumber()
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

        [Fact]
        public async Task MyOrdersAsyncShouldWorkCorrect()
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
                new Order()
                {
                    Id = 4,
                    Comment = "This is my third order",
                    OrderStatus = OrderStatus.Ready,
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            IEnumerable<SingleOrderViewModel> firstUserOrders = await ordersService.GetMyOrdersAsync<SingleOrderViewModel>("2b7462c2-fcda-42b9-87f6-d1fba671afa4", 1);
            IEnumerable<SingleOrderViewModel> secondUserOrders = await ordersService.GetMyOrdersAsync<SingleOrderViewModel>("a39b2980-37dd-4be3-a1bc-4c1399a0578e", 1);
            Assert.NotNull(firstUserOrders);
            Assert.NotNull(secondUserOrders);
            Assert.Equal(3, firstUserOrders.Count());
            Assert.Single(secondUserOrders);
            Assert.Equal("a39b2980-37dd-4be3-a1bc-4c1399a0578e", secondUserOrders.First().ApplicationUserId);
        }

        [Fact]
        public async Task GetAllOrdersAsyncShouldWorkCorrect()
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
                new Order()
                {
                    Id = 4,
                    Comment = "This is my third order",
                    OrderStatus = OrderStatus.Ready,
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            IEnumerable<HotelAdministrationSingleOrderViewModel> allOrdersFirstPage = await ordersService.HotelAdministrationGetAllOrdersAsync<HotelAdministrationSingleOrderViewModel>(1, 2);
            IEnumerable<HotelAdministrationSingleOrderViewModel> allOrdersSecondPage = await ordersService.HotelAdministrationGetAllOrdersAsync<HotelAdministrationSingleOrderViewModel>(2, 2);
            Assert.NotNull(allOrdersFirstPage);
            Assert.NotNull(allOrdersSecondPage);
            Assert.Equal(2, allOrdersFirstPage.Count());
            Assert.Equal(3, allOrdersSecondPage.First().Id);
        }

        [Fact]
        public async Task GetOrdersByOrderStatusAsyncShouldWorkCorrect()
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var ordersService = new OrdersService(this.ordersRepo.Object, this.dishOrdersRepo.Object, this.shoppingCartsRepo.Object);
            IEnumerable<HotelAdministrationSingleOrderViewModel> ordersWithOrderStatusNew = await ordersService.HotelAdministrationGetOrdersByOrderStatusAsync<HotelAdministrationSingleOrderViewModel>(1, OrderStatus.New);
            IEnumerable<HotelAdministrationSingleOrderViewModel> ordersWithOrderStatusReady = await ordersService.HotelAdministrationGetOrdersByOrderStatusAsync<HotelAdministrationSingleOrderViewModel>(1, OrderStatus.Ready);
            Assert.NotNull(ordersWithOrderStatusNew);
            Assert.Equal(2, ordersWithOrderStatusNew.Count());

            Assert.NotNull(ordersWithOrderStatusReady);
            Assert.Single(ordersWithOrderStatusReady);
        }

        // TESTS WITH IN-MEMORY DB
        [Fact]
        public async Task AddCommentToOrderAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            await ordersRepoDB.AddAsync(
            new Order()
            {
                Id = 1,
                Comment = "This is my first order",
                OrderStatus = OrderStatus.New,
                ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
            });
            await ordersRepoDB.AddAsync(
                new Order()
                {
                    Id = 2,
                    Comment = string.Empty,
                    OrderStatus = OrderStatus.Ready,
                    ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                });
            await ordersRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            await ordersService.AddCommentToOrderAsync(2, "This is comment to order 2.");
            Order orderWithComment = ordersRepoDB.AllAsNoTracking().FirstOrDefault(x => x.Id == 2);
            Assert.Equal("This is comment to order 2.", orderWithComment.Comment);
            dbContext.Dispose();
        }

        [Fact]
        public async Task AddOrderAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                    Name = "Dish Test 3",
                    Price = 4.00M,
                    DishCategory = DishCategory.ColdDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_3.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                    Name = "Dish Test 4",
                    Price = 5.00M,
                    DishCategory = DishCategory.AlcoholDrinks,
                    QuantityInStock = 80,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_4.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "f4d00edb-b5f6-4b6b-969a-3128e8eee847",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                });
            await dishesRepoDB.AddAsync(
                new Dish
                {
                    Id = "8bfb0715-1573-4b60-ab0f-e5f9d803de6c",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                });
            await dishesRepoDB.SaveChangesAsync();
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 1,
               Comment = "This is my first order",
               OrderStatus = OrderStatus.New,
               ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 3,
                   },
                   new DishOrder()
                   {
                       DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 54,
                   },
               },
           });
            await ordersRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            ShoppingCartsService shoppingCartsService = new ShoppingCartsService(shoppingCartsRepoDB, dishesRepoDB);
            SingleShoppingCartViewModel firstShoppingCart = new SingleShoppingCartViewModel
            {
                Id = 2,
                ApplicationUserId = "94d46c80-202e-46f0-b762-0f2087b29b41",
                Count = 2,
                DishId = "f4d00edb-b5f6-4b6b-969a-3128e8eee847",
                Dish = new SingleDishViewModel()
                {
                    Id = "f4d00edb-b5f6-4b6b-969a-3128e8eee847",
                    Name = "Dish Test 2",
                    Price = 3.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 100,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                },
            };
            SingleShoppingCartViewModel secondShoppingCart = new SingleShoppingCartViewModel
            {
                Id = 3,
                ApplicationUserId = "94d46c80-202e-46f0-b762-0f2087b29b41",
                Count = 3,
                DishId = "8bfb0715-1573-4b60-ab0f-e5f9d803de6c",
                Dish = new SingleDishViewModel()
                {
                    Id = "8bfb0715-1573-4b60-ab0f-e5f9d803de6c",
                    Name = "Dish Test 1",
                    Price = 2.00M,
                    DishCategory = DishCategory.HotDrinks,
                    QuantityInStock = 50,
                    DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                },
            };
            await shoppingCartsService.AddDishInTheShoppingCartAsync(firstShoppingCart);
            await shoppingCartsService.AddDishInTheShoppingCartAsync(secondShoppingCart);
            await shoppingCartsRepoDB.SaveChangesAsync();
            List<SingleShoppingCartViewModel> shoppingCartsList = new List<SingleShoppingCartViewModel>()
                {
                firstShoppingCart,
                secondShoppingCart,
                };

            AllShoppingCartsOfOneUserViewModel model = new AllShoppingCartsOfOneUserViewModel()
            {
                ShoppingCartsList = shoppingCartsList,
            };
            await ordersService.AddOrderAsync(model, "94d46c80-202e-46f0-b762-0f2087b29b41");
            Assert.Equal(2, await shoppingCartsRepoDB.AllAsNoTracking().CountAsync());
            int count = await ordersService.GetCountAsync();
            Assert.Equal(2, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangeOrderAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 1,
               Comment = "This is my first order",
               OrderStatus = OrderStatus.New,
               ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 3,
                       Dish = new Dish()
                       {
                           Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                           Name = "Dish Test 1",
                           Price = 2.00M,
                           DishCategory = DishCategory.HotDrinks,
                           QuantityInStock = 50,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                       Dish = new Dish()
                       {
                           Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                           Name = "Dish Test 2",
                           Price = 3.00M,
                           DishCategory = DishCategory.HotDrinks,
                           QuantityInStock = 100,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                       },
                   },
               },
           });
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 2,
               Comment = "This is my second order",
               OrderStatus = OrderStatus.TakenToTheGuest,
               ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "0ac6e69e-286e-4e11-9f7a-4a427dafa741",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 4,
                       Dish = new Dish()
                       {
                           Id = "0ac6e69e-286e-4e11-9f7a-4a427dafa741",
                           Name = "Dish Test 3",
                           Price = 7.00M,
                           DishCategory = DishCategory.Salads,
                           IsReady = true,
                           QuantityInStock = 40,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_3.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 4,
                       Dish = new Dish()
                       {
                           Id = "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05",
                           Name = "Dish Test 4",
                           Price = 9.00M,
                           DishCategory = DishCategory.Appetizers,
                           IsReady = true,
                           QuantityInStock = 49,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_4.png",
                       },
                   },
               },
           });
            await ordersRepoDB.SaveChangesAsync();
            await dishOrdersRepoDB.SaveChangesAsync();
            await dishesRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            await ordersService.ChangeStatusOfOrderAsync(2, OrderStatus.TakenToTheGuest);
            await dishesRepoDB.SaveChangesAsync();
            Dish firstDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "0ac6e69e-286e-4e11-9f7a-4a427dafa741");
            Dish secondDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05");
            Assert.False(firstDish.IsReady);
            Assert.False(secondDish.IsReady);
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangeOrderStatusAsyncWhenAllDishesAreReadyShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            EfDeletableEntityRepository<Dish> dishesRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 1,
               Comment = "This is my first order",
               OrderStatus = OrderStatus.New,
               ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 3,
                       Dish = new Dish()
                       {
                           Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                           Name = "Dish Test 1",
                           Price = 2.00M,
                           DishCategory = DishCategory.HotDrinks,
                           QuantityInStock = 50,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                       Dish = new Dish()
                       {
                           Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                           Name = "Dish Test 2",
                           Price = 3.00M,
                           DishCategory = DishCategory.HotDrinks,
                           QuantityInStock = 100,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                       },
                   },
               },
           });
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 2,
               Comment = "This is my second order",
               OrderStatus = OrderStatus.New,
               ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "0ac6e69e-286e-4e11-9f7a-4a427dafa741",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 4,
                       Dish = new Dish()
                       {
                           Id = "0ac6e69e-286e-4e11-9f7a-4a427dafa741",
                           Name = "Dish Test 3",
                           Price = 7.00M,
                           DishCategory = DishCategory.Salads,
                           IsReady = true,
                           QuantityInStock = 40,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_3.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 6,
                       Dish = new Dish()
                       {
                           Id = "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05",
                           Name = "Dish Test 4",
                           Price = 9.00M,
                           DishCategory = DishCategory.Appetizers,
                           IsReady = true,
                           QuantityInStock = 49,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_4.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "0cb92e35-a81b-45d2-ada6-bac1ef78ddd2",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 4,
                       Dish = new Dish()
                       {
                           Id = "0cb92e35-a81b-45d2-ada6-bac1ef78ddd2",
                           Name = "Dish Test 5",
                           Price = 28.00M,
                           DishCategory = DishCategory.MainCourses,
                           IsReady = true,
                           QuantityInStock = 2,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_5.png",
                       },
                   },
               },
           });
            await ordersRepoDB.SaveChangesAsync();
            await dishOrdersRepoDB.SaveChangesAsync();
            await dishesRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            await ordersService.ChangeOrderStatusWhenAllDishesAreReady();
            await dishesRepoDB.SaveChangesAsync();
            Dish firstDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "0ac6e69e-286e-4e11-9f7a-4a427dafa741");
            Dish secondDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05");
            Dish thirdDish = await dishesRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "0cb92e35-a81b-45d2-ada6-bac1ef78ddd2");
            Order orderWithChangedStatus = await ordersRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
            Assert.Equal(36, firstDish.QuantityInStock);
            Assert.Equal(43, secondDish.QuantityInStock);
            Assert.Equal(0, thirdDish.QuantityInStock);
            Assert.Equal(OrderStatus.Ready, orderWithChangedStatus.OrderStatus);
            dbContext.Dispose();
        }

        [Fact]
        public async Task DeleteOrderAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 1,
               Comment = "This is my first order",
               OrderStatus = OrderStatus.New,
               ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 3,
                       Dish = new Dish()
                       {
                           Id = "5ea80afe-706a-4628-8ebb-ef7523da6e8f",
                           Name = "Dish Test 1",
                           Price = 2.00M,
                           DishCategory = DishCategory.HotDrinks,
                           QuantityInStock = 50,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_1.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                       Dish = new Dish()
                       {
                           Id = "45a2a9c8-c7fd-4a33-9c25-fb9e9cd8acfd",
                           Name = "Dish Test 2",
                           Price = 3.00M,
                           DishCategory = DishCategory.HotDrinks,
                           QuantityInStock = 100,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_2.png",
                       },
                   },
               },
           });
            await ordersRepoDB.AddAsync(
           new Order()
           {
               Id = 2,
               Comment = "This is my second order",
               OrderStatus = OrderStatus.Ready,
               ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
               DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "0ac6e69e-286e-4e11-9f7a-4a427dafa741",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 4,
                       Dish = new Dish()
                       {
                           Id = "0ac6e69e-286e-4e11-9f7a-4a427dafa741",
                           Name = "Dish Test 3",
                           Price = 7.00M,
                           DishCategory = DishCategory.Salads,
                           IsReady = true,
                           QuantityInStock = 40,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_3.png",
                       },
                   },
                   new DishOrder()
                   {
                       DishId = "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05",
                       OrderId = 2,
                       ApplicationUserId = "2a46e6aa-3f3d-4504-b9e6-5cc7aef9592f",
                       DishQuantity = 4,
                       Dish = new Dish()
                       {
                           Id = "6ee7c4f6-d09e-43f1-9b5f-71aa61115c05",
                           Name = "Dish Test 4",
                           Price = 9.00M,
                           DishCategory = DishCategory.Appetizers,
                           QuantityInStock = 49,
                           DishImageUrl = "images/dishes/hotDrinks/dish_test_4.png",
                       },
                   },
               },
           });
            await ordersRepoDB.SaveChangesAsync();
            await dishOrdersRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            Assert.Equal(2, await ordersRepoDB.AllAsNoTracking().CountAsync());
            Assert.Equal(4, await dishOrdersRepoDB.AllAsNoTracking().CountAsync());
            await ordersService.DeleteOrderAsync(2);
            Assert.Equal(1, await ordersRepoDB.AllAsNoTracking().CountAsync());
            Assert.Equal(2, await dishOrdersRepoDB.AllAsNoTracking().CountAsync());
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetOrderDetailsAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            EfDeletableEntityRepository<Dish> dishеsRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishеsRepoDB.AddAsync(new Dish
            {
                Id = "7908c242-5723-4fce-96d8-23c3a6f24517",
                Name = "Test dish 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/test-dish-1.png",
            });
            await dishеsRepoDB.AddAsync(new Dish
            {
                Id = "0a536d71-123d-46ec-831e-c57ab22d60d4",
                Name = "Test dish 2",
                Price = 4.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 60,
                DishImageUrl = "images/dishes/hotDrinks/test-dish-2.png",
            });
            await dishеsRepoDB.AddAsync(new Dish
            {
                Id = "6a8aae55-f13a-44f0-b6a2-7eca551a8768",
                Name = "Test dish 3",
                Price = 6.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 70,
                DishImageUrl = "images/dishes/hotDrinks/test-dish-3.png",
            });
            await dishеsRepoDB.SaveChangesAsync();

            await dishImagesRepoDB.AddAsync(new DishImage()
            {
                Id = "5ce25360-94b2-4490-ba00-60604508c6bb",
                DishId = "7908c242-5723-4fce-96d8-23c3a6f24517",
                Extension = "png",
            });
            await dishImagesRepoDB.AddAsync(new DishImage()
            {
                Id = "4075910e-71b9-45c5-96e5-74f90c3c3b5e",
                DishId = "0a536d71-123d-46ec-831e-c57ab22d60d4",
                Extension = "png",
            });
            await dishImagesRepoDB.AddAsync(new DishImage()
            {
                Id = "5f438800-5329-4d09-80c0-2c68cd1f021e",
                DishId = "6a8aae55-f13a-44f0-b6a2-7eca551a8768",
                Extension = "png",
            });
            await dishImagesRepoDB.SaveChangesAsync();

            await ordersRepoDB.AddAsync(
            new Order()
            {
                Id = 1,
                Comment = "This is my first order",
                OrderStatus = OrderStatus.New,
                ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "7908c242-5723-4fce-96d8-23c3a6f24517",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 3,
                   },
                   new DishOrder()
                   {
                       DishId = "0a536d71-123d-46ec-831e-c57ab22d60d4",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                   },
                   new DishOrder()
                   {
                       DishId = "6a8aae55-f13a-44f0-b6a2-7eca551a8768",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                   },
               },
            });

            await ordersRepoDB.SaveChangesAsync();
            await dishOrdersRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            Assert.Equal(3, dishOrdersRepoDB.AllAsNoTracking().Count());
            var existingOrder = await ordersService.GetOrderDetailsAsync<SingleDishOrderViewModel>(1);
            var nonExistingOrder = await ordersService.GetOrderDetailsAsync<SingleDishOrderViewModel>(7);
            HotelAdministrationAddCommentToOrderViewModel hotelAdministrationOrderDetails = await ordersService.GetOrderDetailsToAddCommentAsync(1);
            Assert.NotNull(existingOrder);
            Assert.NotNull(hotelAdministrationOrderDetails);
            Assert.Equal(0, nonExistingOrder.Count());
        }

        [Fact]
        public async Task HotelAdministrationGetOrderDetailsAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("TestOrdersDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Order> ordersRepoDB = new EfDeletableEntityRepository<Order>(dbContext);
            EfDeletableEntityRepository<DishOrder> dishOrdersRepoDB = new EfDeletableEntityRepository<DishOrder>(dbContext);
            EfDeletableEntityRepository<ShoppingCart> shoppingCartsRepoDB = new EfDeletableEntityRepository<ShoppingCart>(dbContext);
            EfDeletableEntityRepository<Dish> dishеsRepoDB = new EfDeletableEntityRepository<Dish>(dbContext);
            EfDeletableEntityRepository<DishImage> dishImagesRepoDB = new EfDeletableEntityRepository<DishImage>(dbContext);
            await dishеsRepoDB.AddAsync(new Dish
            {
                Id = "7908c242-5723-4fce-96d8-23c3a6f24517",
                Name = "Test dish 1",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/test-dish-1.png",
            });
            await dishеsRepoDB.AddAsync(new Dish
            {
                Id = "0a536d71-123d-46ec-831e-c57ab22d60d4",
                Name = "Test dish 2",
                Price = 4.00M,
                DishCategory = DishCategory.ColdDrinks,
                QuantityInStock = 60,
                DishImageUrl = "images/dishes/hotDrinks/test-dish-2.png",
            });
            await dishеsRepoDB.AddAsync(new Dish
            {
                Id = "6a8aae55-f13a-44f0-b6a2-7eca551a8768",
                Name = "Test dish 3",
                Price = 6.00M,
                DishCategory = DishCategory.AlcoholDrinks,
                QuantityInStock = 70,
                DishImageUrl = "images/dishes/hotDrinks/test-dish-3.png",
            });
            await dishеsRepoDB.SaveChangesAsync();

            await dishImagesRepoDB.AddAsync(new DishImage()
            {
                Id = "5ce25360-94b2-4490-ba00-60604508c6bb",
                DishId = "7908c242-5723-4fce-96d8-23c3a6f24517",
                Extension = "png",
            });
            await dishImagesRepoDB.AddAsync(new DishImage()
            {
                Id = "4075910e-71b9-45c5-96e5-74f90c3c3b5e",
                DishId = "0a536d71-123d-46ec-831e-c57ab22d60d4",
                Extension = "png",
            });
            await dishImagesRepoDB.AddAsync(new DishImage()
            {
                Id = "5f438800-5329-4d09-80c0-2c68cd1f021e",
                DishId = "6a8aae55-f13a-44f0-b6a2-7eca551a8768",
                Extension = "png",
            });
            await dishImagesRepoDB.SaveChangesAsync();

            await ordersRepoDB.AddAsync(
            new Order()
            {
                Id = 1,
                Comment = "This is my first order",
                OrderStatus = OrderStatus.New,
                ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                DishOrders = new List<DishOrder>()
               {
                   new DishOrder()
                   {
                       DishId = "7908c242-5723-4fce-96d8-23c3a6f24517",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 3,
                   },
                   new DishOrder()
                   {
                       DishId = "0a536d71-123d-46ec-831e-c57ab22d60d4",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                   },
                   new DishOrder()
                   {
                       DishId = "6a8aae55-f13a-44f0-b6a2-7eca551a8768",
                       OrderId = 1,
                       ApplicationUserId = "2b7462c2-fcda-42b9-87f6-d1fba671afa4",
                       DishQuantity = 5,
                   },
               },
            });

            await ordersRepoDB.SaveChangesAsync();
            await dishOrdersRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            OrdersService ordersService = new OrdersService(ordersRepoDB, dishOrdersRepoDB, shoppingCartsRepoDB);
            Assert.Equal(3, dishOrdersRepoDB.AllAsNoTracking().Count());
            var existingOrder = await ordersService.GetOrderDetailsAsync<SingleDishOrderViewModel>(1);
            var nonExistingOrder = await ordersService.GetOrderDetailsAsync<SingleDishOrderViewModel>(7);
            Assert.NotNull(existingOrder);
            Assert.Equal(0, nonExistingOrder.Count());
        }
    }
}
