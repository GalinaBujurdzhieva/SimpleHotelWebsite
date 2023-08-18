using Microsoft.AspNetCore.Identity;
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
using MyHotelWebsite.Web.ViewModels.Guests.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyHotelWebsite.Services.Data.Tests
{
    public class ReservationsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Reservation>> reservationsRepo;
        private readonly Mock<IDeletableEntityRepository<Room>> roomsRepo;
        private readonly Mock<IDeletableEntityRepository<RoomReservation>> roomReservationsRepo;
        private Mock<UserManager<ApplicationUser>> mockUserManager;

        public ReservationsServiceTests()
        {
            this.reservationsRepo = new Mock<IDeletableEntityRepository<Reservation>>();
            this.roomsRepo = new Mock<IDeletableEntityRepository<Room>>();
            this.roomReservationsRepo = new Mock<IDeletableEntityRepository<RoomReservation>>();
            this.mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task DoesReservationExistsAsyncShouldWorkCorrect()
        {
            IRoomsService roomsService = new RoomsService(this.roomsRepo.Object);
            var reservations = new List<Reservation>
            {
                new Reservation()
                {
                    Id = 1,
                    AccommodationDate = DateTime.UtcNow.AddDays(2),
                    ReleaseDate = DateTime.UtcNow.AddDays(5),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                },
                new Reservation()
                {
                    Id = 2,
                    AccommodationDate = DateTime.UtcNow.AddDays(10),
                    ReleaseDate = DateTime.UtcNow.AddDays(12),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                },
            };
            var mock = reservations.AsQueryable().BuildMock();

            this.reservationsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var reservationsService = new ReservationsService(this.reservationsRepo.Object, this.roomReservationsRepo.Object, roomsService, this.roomsRepo.Object, this.mockUserManager.Object);
            Assert.True(await reservationsService.DoesReservationExistsAsync(2));
            Assert.False(await reservationsService.DoesReservationExistsAsync(5));
        }

        [Fact]
        public async Task GetCountOfPastCurrentActiveAndUpcomingReservationsShouldWorkCorrect()
        {
            IRoomsService roomsService = new RoomsService(this.roomsRepo.Object);
            var reservations = new List<Reservation>
            {
                new Reservation()
                {
                    Id = 1,
                    AccommodationDate = DateTime.UtcNow.AddDays(2),
                    ReleaseDate = DateTime.UtcNow.AddDays(5),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                },
                new Reservation()
                {
                    Id = 2,
                    AccommodationDate = DateTime.UtcNow.AddDays(10),
                    ReleaseDate = DateTime.UtcNow.AddDays(12),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "bfb3ea75-a62c-4247-b20e-3e453ec8313d",
                },
                new Reservation()
                {
                    Id = 3,
                    AccommodationDate = DateTime.UtcNow.AddDays(-10),
                    ReleaseDate = DateTime.UtcNow.AddDays(-6),
                    AdultsCount = 1,
                    ChildrenCount = 2,
                    RoomType = RoomType.Studio,
                    Catering = Catering.BreakfastAndDinner,
                    ApplicationUserId = "3fa3b4b8-498d-443a-987f-3b8c1198d29a",
                },
                new Reservation()
                {
                    Id = 4,
                    AccommodationDate = DateTime.UtcNow.AddDays(-2),
                    ReleaseDate = DateTime.UtcNow.AddDays(2),
                    AdultsCount = 2,
                    ChildrenCount = 2,
                    RoomType = RoomType.Apartment,
                    Catering = Catering.AllInclusive,
                    ApplicationUserId = "c0a57191-00d7-4e3b-9599-d4f9b5aa1ec9",
                },
            };
            var mock = reservations.AsQueryable().BuildMock();

            this.reservationsRepo.Setup(x => x.AllAsNoTracking()).Returns(mock);
            var reservationsService = new ReservationsService(this.reservationsRepo.Object, this.roomReservationsRepo.Object, roomsService, this.roomsRepo.Object, this.mockUserManager.Object);
            Assert.Equal(2, await reservationsService.GetCountOfAllUpcomingReservationsAsync());
            Assert.Equal(1, await reservationsService.GetCountOfAllCurrentReservationsAsync());
            Assert.Equal(1, await reservationsService.GetCountOfAllPastReservationsAsync());
            Assert.True(await reservationsService.IsReservationActiveAtTheMoment(4));
            Assert.False(await reservationsService.IsReservationActiveAtTheMoment(1));
        }

        // TESTS WITH IN-MEMORY DB
        [Fact]
        public async Task AddReservationAsyncShouldWorkCorrectWithDifferentEmailAndPhoneNumber()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            IRoomsService roomsService = new RoomsService(roomsRepoDB);
            EfDeletableEntityRepository<Reservation> reservationsRepoDB = new EfDeletableEntityRepository<Reservation>(dbContext);
            EfDeletableEntityRepository<RoomReservation> roomReservationsRepoDB = new EfDeletableEntityRepository<RoomReservation>(dbContext);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var reservationsService = new ReservationsService(reservationsRepoDB, roomReservationsRepoDB, roomsService, roomsRepoDB, this.mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
            };
            await usersRepoDB.AddAsync(user);
            await usersRepoDB.SaveChangesAsync();

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
            await roomsRepoDB.SaveChangesAsync();

            AddReservationViewModel model = new AddReservationViewModel()
            {
                ReservationEmail = "differentUserEmail@gmail.com",
                ReservationPhone = "00359777999999",
                AccommodationDate = DateTime.UtcNow.AddDays(10),
                ReleaseDate = DateTime.UtcNow.AddDays(12),
                AdultsCount = 1,
                RoomType = RoomType.SingleRoom,
                Catering = Catering.Without,
            };
            Assert.Equal(1, usersRepoDB.AllAsNoTracking().Count());
            this.mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<ApplicationUser>(user));
            await reservationsService.AddReservationAsync(model, "9fabc808-d07d-44d5-9b23-6454705ddd48");
            int count = await reservationsService.GetCountAsync();
            Assert.Equal(1, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task AddReservationAsyncShouldWorkCorrectWithSameEmailAndPhoneNumber()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            IRoomsService roomsService = new RoomsService(roomsRepoDB);
            EfDeletableEntityRepository<Reservation> reservationsRepoDB = new EfDeletableEntityRepository<Reservation>(dbContext);
            EfDeletableEntityRepository<RoomReservation> roomReservationsRepoDB = new EfDeletableEntityRepository<RoomReservation>(dbContext);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var reservationsService = new ReservationsService(reservationsRepoDB, roomReservationsRepoDB, roomsService, roomsRepoDB, this.mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
            };
            await usersRepoDB.AddAsync(user);
            await usersRepoDB.SaveChangesAsync();

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
            await roomsRepoDB.SaveChangesAsync();

            AddReservationViewModel model = new AddReservationViewModel()
            {
                ReservationEmail = "testUser1@gmail.com",
                ReservationPhone = "00359777777777",
                AccommodationDate = DateTime.UtcNow.AddDays(10),
                ReleaseDate = DateTime.UtcNow.AddDays(12),
                AdultsCount = 1,
                RoomType = RoomType.SingleRoom,
                Catering = Catering.Without,
            };
            Assert.Equal(1, usersRepoDB.AllAsNoTracking().Count());
            this.mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<ApplicationUser>(user));
            await reservationsService.AddReservationAsync(model, "9fabc808-d07d-44d5-9b23-6454705ddd48");
            int count = await reservationsService.GetCountAsync();
            Assert.Equal(1, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task DeleteReservationAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            IRoomsService roomsService = new RoomsService(roomsRepoDB);
            EfDeletableEntityRepository<Reservation> reservationsRepoDB = new EfDeletableEntityRepository<Reservation>(dbContext);
            EfDeletableEntityRepository<RoomReservation> roomReservationsRepoDB = new EfDeletableEntityRepository<RoomReservation>(dbContext);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var reservationsService = new ReservationsService(reservationsRepoDB, roomReservationsRepoDB, roomsService, roomsRepoDB, this.mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
            };
            await usersRepoDB.AddAsync(user);
            await usersRepoDB.SaveChangesAsync();

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
            await roomsRepoDB.SaveChangesAsync();
            await reservationsRepoDB.AddAsync(
                new Reservation
                {
                    Id = 1,
                    AccommodationDate = DateTime.UtcNow.AddDays(2),
                    ReleaseDate = DateTime.UtcNow.AddDays(5),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                        },
                    },
                });
            await reservationsRepoDB.AddAsync(
                new Reservation
                {
                    Id = 2,
                    AccommodationDate = DateTime.UtcNow.AddDays(10),
                    ReleaseDate = DateTime.UtcNow.AddDays(15),
                    AdultsCount = 2,
                    RoomType = RoomType.DoubleRoom,
                    Catering = Catering.AllInclusive,
                    ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                        },
                    },
                });
            await reservationsRepoDB.SaveChangesAsync();
            await reservationsService.DeleteReservationAsync(1);
            int count = await reservationsService.GetCountAsync();
            Assert.Equal(1, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task EditReservationAsyncShouldWorkCorrectWithDifferentEmailAndPhoneNumberAsWellAsChangedDatesCateringAndRoomType()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            IRoomsService roomsService = new RoomsService(roomsRepoDB);
            EfDeletableEntityRepository<Reservation> reservationsRepoDB = new EfDeletableEntityRepository<Reservation>(dbContext);
            EfDeletableEntityRepository<RoomReservation> roomReservationsRepoDB = new EfDeletableEntityRepository<RoomReservation>(dbContext);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var reservationsService = new ReservationsService(reservationsRepoDB, roomReservationsRepoDB, roomsService, roomsRepoDB, this.mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
            };
            await usersRepoDB.AddAsync(user);
            await usersRepoDB.SaveChangesAsync();
            this.mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<ApplicationUser>(user));

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
            await roomsRepoDB.SaveChangesAsync();
            await reservationsRepoDB.AddAsync(
                new Reservation
                {
                    Id = 1,
                    AccommodationDate = DateTime.UtcNow.AddDays(2),
                    ReleaseDate = DateTime.UtcNow.AddDays(5),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                        },
                    },
                });
            await reservationsRepoDB.AddAsync(
                new Reservation
                {
                    Id = 2,
                    AccommodationDate = DateTime.UtcNow.AddDays(10),
                    ReleaseDate = DateTime.UtcNow.AddDays(15),
                    AdultsCount = 2,
                    RoomType = RoomType.DoubleRoom,
                    Catering = Catering.AllInclusive,
                    ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                        },
                    },
                });
            await reservationsRepoDB.SaveChangesAsync();

            EditReservationViewModel model = new EditReservationViewModel()
            {
                Id = 2,
                ReservationEmail = "differentUserEmail@gmail.com",
                ReservationPhone = "00359777999999",
                AccommodationDate = DateTime.UtcNow.AddDays(13),
                ReleaseDate = DateTime.UtcNow.AddDays(20),
                AdultsCount = 1,
                RoomType = RoomType.SingleRoom,
                Catering = Catering.BreakfastAndDinner,
            };
            await reservationsService.EditReservationAsync(model, 2, "9fabc808-d07d-44d5-9b23-6454705ddd48");
            Reservation changedReservation = await reservationsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
            Assert.Equal("differentUserEmail@gmail.com", changedReservation.ReservationEmail);
            Assert.Equal("00359777999999", changedReservation.ReservationPhone);
            Assert.Equal(DateTime.UtcNow.AddDays(13).Date, changedReservation.AccommodationDate.Date);
            Assert.Equal(DateTime.UtcNow.AddDays(20).Date, changedReservation.ReleaseDate.Date);
            Assert.Equal(1, changedReservation.AdultsCount);
            Assert.Equal(RoomType.SingleRoom, changedReservation.RoomType);
            Assert.Equal(Catering.BreakfastAndDinner, changedReservation.Catering);
            dbContext.Dispose();
        }

        [Fact]
        public async Task EditReservationAsyncShouldWorkCorrectWithSameEmailAndPhoneNumberAsWellAsChangedDatesCateringAndRoomType()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            EfDeletableEntityRepository<Room> roomsRepoDB = new EfDeletableEntityRepository<Room>(dbContext);
            IRoomsService roomsService = new RoomsService(roomsRepoDB);
            EfDeletableEntityRepository<Reservation> reservationsRepoDB = new EfDeletableEntityRepository<Reservation>(dbContext);
            EfDeletableEntityRepository<RoomReservation> roomReservationsRepoDB = new EfDeletableEntityRepository<RoomReservation>(dbContext);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var reservationsService = new ReservationsService(reservationsRepoDB, roomReservationsRepoDB, roomsService, roomsRepoDB, this.mockUserManager.Object);
            ApplicationUser user = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
            };
            await usersRepoDB.AddAsync(user);
            await usersRepoDB.SaveChangesAsync();
            this.mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<ApplicationUser>(user));

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
            await roomsRepoDB.SaveChangesAsync();
            await reservationsRepoDB.AddAsync(
                new Reservation
                {
                    Id = 1,
                    AccommodationDate = DateTime.UtcNow.AddDays(2),
                    ReleaseDate = DateTime.UtcNow.AddDays(5),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 1,
                            ReservationId = 1,
                            ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                        },
                    },
                });
            await reservationsRepoDB.AddAsync(
                new Reservation
                {
                    Id = 2,
                    AccommodationDate = DateTime.UtcNow.AddDays(10),
                    ReleaseDate = DateTime.UtcNow.AddDays(15),
                    AdultsCount = 2,
                    RoomType = RoomType.DoubleRoom,
                    Catering = Catering.AllInclusive,
                    ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                    RoomReservations = new List<RoomReservation>()
                    {
                        new RoomReservation
                        {
                            RoomId = 2,
                            ReservationId = 2,
                            ApplicationUserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                        },
                    },
                });
            await reservationsRepoDB.SaveChangesAsync();

            EditReservationViewModel model = new EditReservationViewModel()
            {
                Id = 2,
                ReservationEmail = "testUser1@gmail.com",
                ReservationPhone = "00359777777777",
                AccommodationDate = DateTime.UtcNow.AddDays(13),
                ReleaseDate = DateTime.UtcNow.AddDays(20),
                AdultsCount = 1,
                RoomType = RoomType.SingleRoom,
                Catering = Catering.BreakfastAndDinner,
            };
            await reservationsService.EditReservationAsync(model, 2, "9fabc808-d07d-44d5-9b23-6454705ddd48");
            Reservation changedReservation = await reservationsRepoDB.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
            Assert.Equal("testUser1@gmail.com", changedReservation.ReservationEmail);
            Assert.Equal("00359777777777", changedReservation.ReservationPhone);
            Assert.Equal(DateTime.UtcNow.AddDays(13).Date, changedReservation.AccommodationDate.Date);
            Assert.Equal(DateTime.UtcNow.AddDays(20).Date, changedReservation.ReleaseDate.Date);
            Assert.Equal(1, changedReservation.AdultsCount);
            Assert.Equal(RoomType.SingleRoom, changedReservation.RoomType);
            Assert.Equal(Catering.BreakfastAndDinner, changedReservation.Catering);
            dbContext.Dispose();
        }
    }
}
