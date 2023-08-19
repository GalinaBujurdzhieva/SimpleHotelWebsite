namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;
    using Xunit;

    public class GuestsServiceTests
    {
        [Fact]
        public async Task GetCountAsyncShouldReturnAllGuestsCount()
        {
            Mock<UserManager<ApplicationUser>> mockUserManager = new Mock<UserManager<ApplicationUser>>(
               Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            Mock<RoleManager<ApplicationRole>> mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
            Mock<IDeletableEntityRepository<ApplicationUser>> usersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            Mock<IDeletableEntityRepository<ApplicationRole>> rolesRepo = new Mock<IDeletableEntityRepository<ApplicationRole>>();

            IList<ApplicationUser> guests = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "fd607992-52e3-43ce-a9de-7137db6d19a1",
                    UserName = "Guest1",
                    Email = "Guest1@gmail.com",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    PhoneNumber = "00359888112233",
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "d8f76e81-db91-4d2e-85f0-cd402b763e86",
                    UserName = "Guest2",
                    Email = "Guest2@gmail.com",
                    FirstName = "Petar",
                    LastName = "Petrov",
                    PhoneNumber = "00359999332211",
                    EmailConfirmed = true,
                },
            };

            var mock = guests.AsQueryable().BuildMock();
            usersRepo.Setup(u => u.AllAsNoTracking()).Returns(mock);
            var guestService = new GuestsService(usersRepo.Object, rolesRepo.Object, mockUserManager.Object, mockRoleManager.Object);

            mockUserManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>())).Returns(Task.FromResult(guests));
            int guestsCount = await guestService.GetCountAsync();
            Assert.Equal(2, guestsCount);
        }

        // TESTS WITH IN-MEMORY DB
        [Fact]
        public async Task GetAllGuestsAsyncShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            Mock<UserManager<ApplicationUser>> mockUserManager = new Mock<UserManager<ApplicationUser>>(
               Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            Mock<RoleManager<ApplicationRole>> mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            EfDeletableEntityRepository<ApplicationRole> rolesRepoDB = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "2baec4cd-2b4c-491b-8679-a4a3e5bbd46b",
                    Name = GlobalConstants.HotelManagerRoleName,
                });
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "3d3a64bc-f885-4f6b-be56-abf050a4a5ff",
                    Name = GlobalConstants.ChefRoleName,
                });
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "f3521ccd-9d7c-4f62-8767-50d101f0ff90",
                    Name = GlobalConstants.WaiterRoleName,
                });
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "55359ebf-f641-4064-beca-8156aeedb42f",
                    Name = GlobalConstants.MaidRoleName,
                });
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "1159da12-1d8e-4a65-8701-c8be3f9c8ce2",
                    Name = GlobalConstants.ReceptionistRoleName,
                });
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "374d7029-41e6-443a-a364-2118694f8a3e",
                    Name = GlobalConstants.WebsiteAdministratorRoleName,
                });
            await rolesRepoDB.AddAsync(
                new ApplicationRole()
                {
                    Id = "7ba79047-a04b-445b-a0c0-8efd401a7154",
                    Name = GlobalConstants.GuestRoleName,
                });
            await rolesRepoDB.SaveChangesAsync();

            await usersRepoDB.AddAsync(
            new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
                Role = GlobalConstants.GuestRoleName,
            });
            await usersRepoDB.AddAsync(
            new ApplicationUser()
            {
                Id = "585a155e-41c0-42b3-b4a2-acc0cd35408a",
                UserName = "TestUser2",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser2@gmail.com",
                FirstName = "Peshko",
                LastName = "Peshev",
                PhoneNumber = "00359777000111",
                EmailConfirmed = true,
                Role = GlobalConstants.GuestRoleName,
            });
            await usersRepoDB.AddAsync(
            new ApplicationUser()
            {
                Id = "ff47ea75-3821-4c2d-8c87-168b074f8236",
                UserName = "TestUser3",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser3@gmail.com",
                FirstName = "Ivcho",
                LastName = "Ivov",
                PhoneNumber = "00359888333333",
                EmailConfirmed = true,
                Role = GlobalConstants.ChefRoleName,
            });

            await usersRepoDB.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var guestService = new GuestsService(usersRepoDB, rolesRepoDB, mockUserManager.Object, mockRoleManager.Object);
            var guests = await guestService.GetAllGuestsAsync<SingleGuestViewModel>(1, 7);
            Assert.Equal(2, guests.Count());
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetGuestEmailAndPhoneNumberShouldWorkCorrect()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("TestDishesDb");
            ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            Mock<UserManager<ApplicationUser>> mockUserManager = new Mock<UserManager<ApplicationUser>>(
               Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            Mock<RoleManager<ApplicationRole>> mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
            EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            EfDeletableEntityRepository<ApplicationRole> rolesRepoDB = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            ApplicationUser appUser = new ApplicationUser()
            {
                UserName = "TestUser1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "testUser1@gmail.com",
                FirstName = "Goshko",
                LastName = "Goshev",
                PhoneNumber = "00359777777777",
                EmailConfirmed = true,
            };
            await usersRepoDB.AddAsync(appUser);
            await usersRepoDB.SaveChangesAsync();

            var guestService = new GuestsService(usersRepoDB, rolesRepoDB, mockUserManager.Object, mockRoleManager.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "9fabc808-d07d-44d5-9b23-6454705ddd48"),
            }));
            mockUserManager.Setup(_ => _.GetUserAsync(user)).ReturnsAsync(appUser);
            Assert.Equal("testUser1@gmail.com", await guestService.GetGuestEmailAsync(user));
            Assert.Equal("00359777777777", await guestService.GetGuestPhoneNumberAsync(user));
            dbContext.Dispose();
        }
    }
}
