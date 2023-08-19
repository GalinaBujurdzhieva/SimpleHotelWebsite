namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Data.Repositories;
    using MyHotelWebsite.Services.Mapping;
    using Xunit;

    public class StaffServiceTests
    {
        [Fact]
        public async Task LockEmployeeAsyncShouldWorkCorrectWhenThereIsNoPreviousLockOutDateAndThrowsExceptionWhenEmployeeDoesNotExistOrIsLocked()
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var staffService = new StaffService(usersRepoDB, mockUserManager.Object, mockRoleManager.Object, dbContext);
            ApplicationUser user1 = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestEmployee1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "TestEmployee1@gmail.com",
                FirstName = "Joro",
                LastName = "Gotiniq",
                PhoneNumber = "00359777007007",
                EmailConfirmed = true,
                LockoutEnabled = false,
            };
            await usersRepoDB.AddAsync(user1);
            ApplicationUser user2 = new ApplicationUser()
            {
                Id = "144e1710-ffcc-4674-97b6-c023e6b8fca0",
                UserName = "TestEmployee2",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "TestEmployee2@gmail.com",
                FirstName = "Pipi",
                LastName = "Petkova",
                PhoneNumber = "00359333003003",
                EmailConfirmed = true,
                LockoutEnd = DateTime.UtcNow.AddDays(100).Date,
            };
            await usersRepoDB.AddAsync(user2);
            await usersRepoDB.SaveChangesAsync();
            mockUserManager.Setup(u => u.FindByIdAsync("9fabc808-d07d-44d5-9b23-6454705ddd48")).Returns(Task.FromResult<ApplicationUser>(user1));
            mockUserManager.Setup(u => u.FindByIdAsync("144e1710-ffcc-4674-97b6-c023e6b8fca0")).Returns(Task.FromResult<ApplicationUser>(user2));
            await staffService.LockUser("9fabc808-d07d-44d5-9b23-6454705ddd48");
            Assert.Equal(DateTime.UtcNow.AddYears(1000).Date, user1.LockoutEnd.Value.Date);
            await Assert.ThrowsAsync<Exception>(async () => await staffService.LockUser("6acd1018-da72-4b55-aec1-7456ce025737"));
            await Assert.ThrowsAsync<Exception>(async () => await staffService.LockUser("11ae10e0-6137-4f47-984b-f0b45b2051f0"));
            dbContext.Dispose();
        }

        [Fact]
        public async Task LockEmployeeAsyncShouldWorkCorrectWithPreviousLockOutDate()
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var staffService = new StaffService(usersRepoDB, mockUserManager.Object, mockRoleManager.Object, dbContext);
            ApplicationUser user3 = new ApplicationUser()
            {
                Id = "11ae10e0-6137-4f47-984b-f0b45b2051f0",
                UserName = "TestEmployee3",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "TestEmployee3@gmail.com",
                FirstName = "Galka",
                LastName = "Tosheva",
                PhoneNumber = "00359222002002",
                EmailConfirmed = true,
                LockoutEnd = DateTime.UtcNow.AddDays(-10).Date,
            };
            await usersRepoDB.AddAsync(user3);
            await usersRepoDB.SaveChangesAsync();
            mockUserManager.Setup(u => u.FindByIdAsync("11ae10e0-6137-4f47-984b-f0b45b2051f0")).Returns(Task.FromResult<ApplicationUser>(user3));
            await staffService.LockUser("11ae10e0-6137-4f47-984b-f0b45b2051f0");
            Assert.Equal(DateTime.UtcNow.AddYears(1000).Date, user3.LockoutEnd.Value.Date);
            dbContext.Dispose();
        }

        [Fact]
        public async Task UnLockEmployeeAsyncShouldWorkCorrectWhenEmployeeIsLockedAndThrowsExceptionWhenEmployeeDoesNotExist()
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var staffService = new StaffService(usersRepoDB, mockUserManager.Object, mockRoleManager.Object, dbContext);
            ApplicationUser user1 = new ApplicationUser()
            {
                Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
                UserName = "TestEmployee1",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "TestEmployee1@gmail.com",
                FirstName = "Joro",
                LastName = "Gotiniq",
                PhoneNumber = "00359777007007",
                EmailConfirmed = true,
                LockoutEnd = DateTime.UtcNow.AddMonths(13).Date,
            };
            await usersRepoDB.AddAsync(user1);
            await usersRepoDB.SaveChangesAsync();
            mockUserManager.Setup(u => u.FindByIdAsync("9fabc808-d07d-44d5-9b23-6454705ddd48")).Returns(Task.FromResult<ApplicationUser>(user1));
            await staffService.UnlockUser("9fabc808-d07d-44d5-9b23-6454705ddd48");
            Assert.Equal(DateTime.UtcNow.Date, user1.LockoutEnd.Value.Date);
            await Assert.ThrowsAsync<Exception>(async () => await staffService.UnlockUser("11ae10e0-6137-4f47-984b-f0b45b2051f0"));
            dbContext.Dispose();
        }

        [Fact]
        public async Task UnLockEmployeeAsyncShouldThrowExceptionWhenUserIsUnlocked()
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
            AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
            var staffService = new StaffService(usersRepoDB, mockUserManager.Object, mockRoleManager.Object, dbContext);
            ApplicationUser user3 = new ApplicationUser()
            {
                Id = "11ae10e0-6137-4f47-984b-f0b45b2051f0",
                UserName = "TestEmployee3",
                PasswordHash = Guid.NewGuid().ToString(),
                Email = "TestEmployee3@gmail.com",
                FirstName = "Galka",
                LastName = "Tosheva",
                PhoneNumber = "00359222002002",
                EmailConfirmed = true,
                LockoutEnd = DateTime.UtcNow.AddDays(-20).Date,
            };
            await usersRepoDB.AddAsync(user3);
            await usersRepoDB.SaveChangesAsync();
            mockUserManager.Setup(u => u.FindByIdAsync("11ae10e0-6137-4f47-984b-f0b45b2051f0")).Returns(Task.FromResult<ApplicationUser>(user3));
            await Assert.ThrowsAsync<Exception>(async () => await staffService.UnlockUser("11ae10e0-6137-4f47-984b-f0b45b2051f0"));
            dbContext.Dispose();
        }
    }
}
