namespace MyHotelWebsite.Services.Data.Tests
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class GuestsServiceTests
    {
        //private Mock<UserManager<ApplicationUser>> mockUserManager;
        //private Mock<RoleManager<ApplicationRole>> mockRoleManager;

        //public ApplicationDbContext GetDbContext()
        //{
        //    DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase("TestGuest");
        //    ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
        //    return dbContext;
        //}

        //public GuestsService GetGuestsService()
        //{
        //    this.mockUserManager = new Mock<UserManager<ApplicationUser>>(
        //        Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        //    this.mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
        //    EfDeletableEntityRepository<ApplicationUser> usersRepo = new EfDeletableEntityRepository<ApplicationUser>(this.GetDbContext());
        //    GuestsService guestsService = new GuestsService(usersRepo, this.mockUserManager.Object, this.mockRoleManager.Object);
        //    return guestsService;
        //}

        [Fact]
        public async Task GetCountAsyncShouldReturnAllGuestsCount()
        {
            Mock<UserManager<ApplicationUser>> mockUserManager = new Mock<UserManager<ApplicationUser>>(
               Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            Mock<RoleManager<ApplicationRole>> mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
            Mock<IDeletableEntityRepository<ApplicationUser>> usersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();

            var guests = new List<ApplicationUser>
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

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.AddToRoleAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>())).ReturnsAsync((ApplicationUser user, string role) =>
                {
                    role = GlobalConstants.GuestRoleName.ToString();
                    return IdentityResult.Success;
                });
            mockUserManager.Setup(x => x.IsInRoleAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>())).ReturnsAsync((ApplicationUser user, string role) =>
                {
                    role = GlobalConstants.GuestRoleName.ToString();
                    return true;
                });
            int guestsCount = await guestService.GetCountAsync();
            Assert.Equal(2, guestsCount);
        }
    }
}
