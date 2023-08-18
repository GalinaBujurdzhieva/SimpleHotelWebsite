namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Repositories;
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
            var guestService = new GuestsService(usersRepo.Object, mockUserManager.Object, mockRoleManager.Object);

            mockUserManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>())).Returns(Task.FromResult(guests));
            int guestsCount = await guestService.GetCountAsync();
            Assert.Equal(2, guestsCount);
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
            ApplicationUser appUser = new ApplicationUser()
            {
                // Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
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

            var guestService = new GuestsService(usersRepoDB, mockUserManager.Object, mockRoleManager.Object);
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
