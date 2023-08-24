namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;
    using MyHotelWebsite.Web.ViewModels.Blogs;
    using Xunit;

    public class RoomsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Room>> roomsRepo;

        public RoomsServiceTests()
        {
            this.roomsRepo = new Mock<IDeletableEntityRepository<Room>>();
        }

        [Fact]
        public async Task DoesRoomExistsAsyncShouldWorkCorrectly()
        {
            var rooms = new List<Room>()
            {
            new Room
            {
                Id = 1,
                RoomNumber = 1,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            },
            new Room
            {
                Id = 2,
                RoomNumber = 2,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            },
            };
            var mock = rooms.AsQueryable().BuildMock();

            this.roomsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var roomsService = new RoomsService(this.roomsRepo.Object);
            Assert.True(await roomsService.DoesRoomExistsAsync(2));
            Assert.False(await roomsService.DoesRoomExistsAsync(5));
        }

        [Fact]
        public async Task GetAllRoomsAsyncShouldWorkCorrectly()
        {
            var rooms = new List<Room>()
            {
            new Room
            {
                Id = 1,
                RoomNumber = 1,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            },
            new Room
            {
                Id = 2,
                RoomNumber = 2,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            },
            new Room
            {
                Id = 3,
                RoomNumber = 3,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            },
            new Room
            {
                Id = 4,
                RoomNumber = 4,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            },
            };
            var mock = rooms.AsQueryable().BuildMock();
            this.roomsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var roomsService = new RoomsService(this.roomsRepo.Object);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            IEnumerable<SingleRoomViewModel> allRoomsFirstPage = await roomsService.GetAllRoomsAsync<SingleRoomViewModel>(1, 3);
            IEnumerable<SingleRoomViewModel> allRoomsSecondPage = await roomsService.GetAllRoomsAsync<SingleRoomViewModel>(2, 3);
            Assert.NotNull(allRoomsFirstPage);
            Assert.NotNull(allRoomsSecondPage);
            Assert.Equal(3, allRoomsFirstPage.Count());
            Assert.Equal(4, allRoomsSecondPage.First().Id);
        }

        [Fact]
        public async Task GetCountOfRoomsByFourCriteriaAsyncShouldWorkCorrectly()
        {
            var rooms = new List<Room>()
            {
            new Room
            {
                Id = 1,
                RoomNumber = 1,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
                IsReserved = true,
            },
            new Room
            {
                Id = 2,
                RoomNumber = 2,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
                IsOccupied = true,
            },
            new Room
            {
                Id = 3,
                RoomNumber = 3,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
                IsCleaned = true,
            },
            new Room
            {
                Id = 4,
                RoomNumber = 4,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            },
            };
            var mock = rooms.AsQueryable().BuildMock();
            this.roomsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var roomsService = new RoomsService(this.roomsRepo.Object);
            Assert.Equal(2, await roomsService.GetCountOfRoomsByFourCriteriaAsync(false, false, false, RoomType.DoubleRoom));
            Assert.Equal(1, await roomsService.GetCountOfRoomsByFourCriteriaAsync(false, true, false, RoomType.DoubleRoom));
            Assert.Equal(1, await roomsService.GetCountOfRoomsByFourCriteriaAsync(true, false, false, RoomType.SingleRoom));
            Assert.Equal(1, await roomsService.GetCountOfRoomsByFourCriteriaAsync(false, false, true));
        }

        [Fact]
        public async Task SearchingRoomsByFourCriteriaAsyncShouldWorkCorrectly()
        {
            var rooms = new List<Room>()
            {
            new Room
            {
                Id = 1,
                RoomNumber = 1,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
                IsReserved = true,
            },
            new Room
            {
                Id = 2,
                RoomNumber = 2,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
                IsOccupied = true,
            },
            new Room
            {
                Id = 3,
                RoomNumber = 3,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
                IsCleaned = true,
            },
            new Room
            {
                Id = 4,
                RoomNumber = 4,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            },
            };
            var mock = rooms.AsQueryable().BuildMock();
            this.roomsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var roomsService = new RoomsService(this.roomsRepo.Object);
            IEnumerable<SingleRoomViewModel> searchingListWithDoubleRooms = await roomsService.SearchRoomsByFourCriteriaAsync<SingleRoomViewModel>(1, false, false, false, RoomType.DoubleRoom);
            IEnumerable<SingleRoomViewModel> searchingListWithOccupiedDoubleRooms = await roomsService.SearchRoomsByFourCriteriaAsync<SingleRoomViewModel>(1, false, true, false, RoomType.DoubleRoom);
            IEnumerable<SingleRoomViewModel> searchingListWithReservedSingleRooms = await roomsService.SearchRoomsByFourCriteriaAsync<SingleRoomViewModel>(1, true, false, false, RoomType.SingleRoom);
            IEnumerable<SingleRoomViewModel> searchingListWithCleanedRooms = await roomsService.SearchRoomsByFourCriteriaAsync<SingleRoomViewModel>(1, false, false, true);

            Assert.NotNull(searchingListWithDoubleRooms);
            Assert.Equal(2, searchingListWithDoubleRooms.Count());
            Assert.NotNull(searchingListWithOccupiedDoubleRooms);
            Assert.Single(searchingListWithOccupiedDoubleRooms);
            Assert.NotNull(searchingListWithReservedSingleRooms);
            Assert.Single(searchingListWithReservedSingleRooms);
            Assert.NotNull(searchingListWithCleanedRooms);
            Assert.Single(searchingListWithCleanedRooms);
        }

        [Fact]
        public async Task GetCountAsyncShouldWorkCorrectly()
        {
            var rooms = new List<Room>()
            {
            new Room(),
            new Room(),
            new Room(),
            new Room(),
            };
            var mock = rooms.AsQueryable().BuildMock();
            this.roomsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var roomsService = new RoomsService(this.roomsRepo.Object);
            Assert.Equal(4, await roomsService.GetCountAsync());
        }

        // TESTS WITH IN-MEMORY DB
        [Fact]
        public async Task CleanRoomAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
               });
            await roomsRepoDB.SaveChangesAsync();
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            await roomsService.CleanRoomAsync(1, "64894e0f-e3be-4ae9-9129-169af6053493");
            Room cleanedRoom = await roomsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == 1);
            Assert.True(cleanedRoom.IsCleaned);
            await Assert.ThrowsAsync<Exception>(() => roomsService.CleanRoomAsync(2, "64894e0f-e3be-4ae9-9129-169af6053493"));
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetAdultsCountAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 2,
                   Floor = 0,
                   RoomType = RoomType.DoubleRoom,
                   AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
               });
            await roomsRepoDB.SaveChangesAsync();
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            Assert.Equal(1, await roomsService.GetAdultsCountAsync(1));
            Assert.Equal(2, await roomsService.GetAdultsCountAsync(2));
            Assert.Equal(3, await roomsService.GetAdultsCountAsync(3));
            Assert.Equal(4, await roomsService.GetAdultsCountAsync(4));
            dbContext.Dispose();
        }

        [Fact]
        public async Task GelAllFreeRoomsAtTheMomentAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(10),
                                ReleaseDate = DateTime.UtcNow.AddDays(15),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(4),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            IEnumerable<SingleRoomViewModel> freeRoomsAtTheMoment = await roomsService.GetAllFreeRoomsAtTheMomentAsync<SingleRoomViewModel>();
            Assert.NotNull(freeRoomsAtTheMoment);
            Assert.Equal(2, freeRoomsAtTheMoment.Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task GelAllFreeRoomsForACertainPeriodOfTimeAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(4),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            IEnumerable<SingleRoomViewModel> freeRoomsForACertainPeriodOfTime = await roomsService.GetAllFreeRoomsForACertainPeriodOfTimeAsync<SingleRoomViewModel>(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3));
            IEnumerable<SingleRoomViewModel> freeRoomsForAnotherPeriodOfTime = await roomsService.GetAllFreeRoomsForACertainPeriodOfTimeAsync<SingleRoomViewModel>(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(8));
            Assert.NotNull(freeRoomsForACertainPeriodOfTime);
            Assert.Single(freeRoomsForACertainPeriodOfTime);
            Assert.NotNull(freeRoomsForAnotherPeriodOfTime);
            Assert.Equal(2, freeRoomsForAnotherPeriodOfTime.Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetRoomTypeByIdAsyncShouldWorkCorrectly()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 2,
                   Floor = 0,
                   RoomType = RoomType.DoubleRoom,
                   AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
                   IsCleaned = true,
               });
            await roomsRepoDB.SaveChangesAsync();
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            Assert.Equal(RoomType.SingleRoom, await roomsService.GetRoomTypeByIdAsync(1));
            Assert.Equal(RoomType.DoubleRoom, await roomsService.GetRoomTypeByIdAsync(2));
            await Assert.ThrowsAsync<Exception>(async () => await roomsService.GetRoomTypeByIdAsync(12));
            dbContext.Dispose();
        }

        [Fact]
        public async Task LeaveOccupiedRoomsAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    IsOccupied = true,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   IsOccupied = true,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   IsOccupied = true,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   IsOccupied = true,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(4),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            var occupiedRooms = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsOccupied).ToListAsync();
            Assert.NotNull(occupiedRooms);
            Assert.Equal(4, occupiedRooms.Count());
            await roomsService.LeaveOccupiedRoomsAsync();
            var occupiedRoomsNow = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsOccupied).ToListAsync();
            Assert.Equal(2, occupiedRoomsNow.Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task OccupyRoomsAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(4),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            var notOccupiedRooms = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsOccupied == false).ToListAsync();
            Assert.NotNull(notOccupiedRooms);
            Assert.Equal(4, notOccupiedRooms.Count());
            await roomsService.OccupyRoomsAsync();
            var occupiedRoomsNow = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsOccupied).ToListAsync();
            Assert.Equal(2, occupiedRoomsNow.Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task RemoveIsReservedPropertyOfNotReservedRoomsShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    IsReserved = true,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   IsReserved = true,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   IsReserved = true,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   IsReserved = true,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(-1),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            var reservedRooms = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsReserved).ToListAsync();
            Assert.NotNull(reservedRooms);
            Assert.Equal(4, reservedRooms.Count());
            await roomsService.RemoveIsReservedPropertyOfNotReservedRooms();
            var reservedRoomsNow = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsReserved).ToListAsync();
            Assert.Equal(3, reservedRoomsNow.Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task ReserveRoomAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(-1),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            var notReservedRooms = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsReserved == false).ToListAsync();
            Assert.NotNull(notReservedRooms);
            Assert.Equal(4, notReservedRooms.Count());
            await roomsService.ReserveRoomAsync(RoomType.Studio, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(8));
            var reservedRoomsNow = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsReserved).ToListAsync();
            Assert.Single(reservedRoomsNow);
            await Assert.ThrowsAsync<Exception>(() => roomsService.ReserveRoomAsync(RoomType.DoubleRoom, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(8)));
            dbContext.Dispose();
        }

        [Fact]
        public async Task ReserveRoomByIdAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(-1),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            var notReservedRooms = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsReserved == false).ToListAsync();
            Assert.NotNull(notReservedRooms);
            Assert.Equal(4, notReservedRooms.Count());
            await roomsService.ReserveRoomByIdAsync(3, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(8));
            var reservedRoomsNow = await roomsRepoDB.AllAsNoTracking().Where(x => x.IsReserved).ToListAsync();
            Assert.Single(reservedRoomsNow);
            await Assert.ThrowsAsync<Exception>(() => roomsService.ReserveRoomByIdAsync(8, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(8)));
            dbContext.Dispose();
        }

        [Fact]
        public async Task TurnIsReservedPropertyToTrueAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestRoomsDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            await roomsRepoDB.AddAsync(
                new Room
                {
                    Id = 1,
                    RoomNumber = 1,
                    Capacity = 1,
                    Floor = 0,
                    RoomType = RoomType.SingleRoom,
                    AdultPrice = GlobalConstants.SingleRoomPrice,
                    ChildrenPrice = GlobalConstants.SingleRoomPrice,
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            Reservation = new Reservation()
                            {
                                Id = 1,
                                AccommodationDate = DateTime.UtcNow.AddDays(2),
                                ReleaseDate = DateTime.UtcNow.AddDays(12),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                            },
                        },
                    },
                });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 2,
                   RoomNumber = 2,
                   Capacity = 1,
                   Floor = 0,
                   RoomType = RoomType.SingleRoom,
                   AdultPrice = GlobalConstants.SingleRoomPrice,
                   ChildrenPrice = GlobalConstants.SingleRoomPrice,
                   IsCleaned = true,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            Reservation = new Reservation()
                            {
                                Id = 2,
                                AccommodationDate = DateTime.UtcNow.AddDays(4),
                                ReleaseDate = DateTime.UtcNow.AddDays(10),
                                AdultsCount = 1,
                                RoomType = RoomType.SingleRoom,
                                Catering = Catering.Without,
                                ApplicationUserId = "a02a70f4-b8f7-4787-b6c2-40209fdf1aab",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 3,
                   RoomNumber = 3,
                   Capacity = 3,
                   Floor = 0,
                   RoomType = RoomType.Studio,
                   AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 3,
                            ReservationId = 3,
                            Reservation = new Reservation()
                            {
                                Id = 3,
                                AccommodationDate = DateTime.UtcNow.AddDays(-2),
                                ReleaseDate = DateTime.UtcNow.AddDays(3),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "a82bcfe3-fdf4-4ffb-ab42-719e4c7cbec9",
                            },
                        },
                    },
               });
            await roomsRepoDB.AddAsync(
               new Room
               {
                   Id = 4,
                   RoomNumber = 4,
                   Capacity = 4,
                   Floor = 0,
                   RoomType = RoomType.Apartment,
                   AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                   ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
                   RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 4,
                            ReservationId = 4,
                            Reservation = new Reservation()
                            {
                                Id = 4,
                                AccommodationDate = DateTime.UtcNow.AddDays(-3),
                                ReleaseDate = DateTime.UtcNow.AddDays(-1),
                                AdultsCount = 2,
                                ChildrenCount = 1,
                                RoomType = RoomType.Studio,
                                Catering = Catering.Without,
                                ApplicationUserId = "424a2f5f-4bbe-43eb-82dc-4363b46d3a0c",
                            },
                        },
                    },
               });
            await roomsRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            RoomsService roomsService = new RoomsService(roomsRepoDB);
            await roomsService.TurnTrueIsReservedPropertyOfReservedRooms();
            Room firstRoom = await roomsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(r => r.Id == 1);
            Room secondRoom = await roomsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(r => r.Id == 2);
            Room thirdRoom = await roomsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(r => r.Id == 3);
            Room fourthRoom = await roomsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(r => r.Id == 4);
            Assert.True(firstRoom.IsReserved);
            Assert.True(secondRoom.IsReserved);
            Assert.True(thirdRoom.IsReserved);
            Assert.False(fourthRoom.IsReserved);
        }
    }
}