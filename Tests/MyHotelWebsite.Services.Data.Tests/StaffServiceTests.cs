namespace MyHotelWebsite.Services.Data.Tests
{
    using System;
    using System.Collections;
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
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;
    using MyHotelWebsite.Web.ViewModels.Administration.Staff;
    using Xunit;

    public class StaffServiceTests
    {
        //[Fact]
        //public async Task GetAllEmployeesAsyncShouldWorkCorrect()
        //{
        //    DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
        //       .UseInMemoryDatabase("TestDishesDb");
        //    ApplicationDbContext dbContext = new ApplicationDbContext(optionBuilder.Options);
        //    dbContext.Database.EnsureDeleted();
        //    dbContext.Database.EnsureCreated();
        //    Mock<UserManager<ApplicationUser>> mockUserManager = new Mock<UserManager<ApplicationUser>>(
        //       Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        //    Mock<RoleManager<ApplicationRole>> mockRoleManager = new Mock<RoleManager<ApplicationRole>>(Mock.Of<IRoleStore<ApplicationRole>>(), null, null, null, null);
        //    EfDeletableEntityRepository<ApplicationUser> usersRepoDB = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

        //    var roles = new List<ApplicationRole>()
        //    {
        //        new ApplicationRole()
        //        {
        //            Id = "2baec4cd-2b4c-491b-8679-a4a3e5bbd46b",
        //            Name = GlobalConstants.HotelManagerRoleName,
        //        },
        //        new ApplicationRole()
        //        {
        //            Id = "3d3a64bc-f885-4f6b-be56-abf050a4a5ff",
        //            Name = GlobalConstants.ChefRoleName,
        //        },
        //        new ApplicationRole()
        //        {
        //            Id = "f3521ccd-9d7c-4f62-8767-50d101f0ff90",
        //            Name = GlobalConstants.WaiterRoleName,
        //        },
        //        new ApplicationRole()
        //        {
        //            Id = "55359ebf-f641-4064-beca-8156aeedb42f",
        //            Name = GlobalConstants.MaidRoleName,
        //        },
        //        new ApplicationRole()
        //        {
        //            Id = "1159da12-1d8e-4a65-8701-c8be3f9c8ce2",
        //            Name = GlobalConstants.ReceptionistRoleName,
        //        },
        //        new ApplicationRole()
        //        {
        //            Id = "374d7029-41e6-443a-a364-2118694f8a3e",
        //            Name = GlobalConstants.WebsiteAdministratorRoleName,
        //        },
        //        new ApplicationRole()
        //        {
        //            Id = "7ba79047-a04b-445b-a0c0-8efd401a7154",
        //            Name = GlobalConstants.GuestRoleName,
        //        },
        //    }.AsQueryable().AsAsyncEnumerable();

        //    var users = new List<ApplicationUser>()
        //    {
        //        new ApplicationUser()
        //    {
        //        Id = "9fabc808-d07d-44d5-9b23-6454705ddd48",
        //        UserName = "Guest",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "testGuest@gmail.com",
        //        FirstName = "Goshko",
        //        LastName = "Goshev",
        //        PhoneNumber = "00359777777777",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.GuestRoleName,
        //    },
        //        new ApplicationUser()
        //    {
        //        Id = "585a155e-41c0-42b3-b4a2-acc0cd35408a",
        //        UserName = "Chef",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "Chef@gmail.com",
        //        FirstName = "Peshko",
        //        LastName = "Peshev",
        //        PhoneNumber = "00359777000111",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.ChefRoleName,
        //    },
        //        new ApplicationUser()
        //    {
        //        Id = "ff47ea75-3821-4c2d-8c87-168b074f8236",
        //        UserName = "Waiter",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "Waiter@gmail.com",
        //        FirstName = "Ivcho",
        //        LastName = "Ivov",
        //        PhoneNumber = "00359888333333",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.WaiterRoleName,
        //    },
        //        new ApplicationUser()
        //    {
        //        Id = "b671dd2b-f12c-48de-a523-1164bb799880",
        //        UserName = "Receptionist",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "Receptionist@gmail.com",
        //        FirstName = "Vasko",
        //        LastName = "Popov",
        //        PhoneNumber = "00359888112233",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.ReceptionistRoleName,
        //    },
        //        new ApplicationUser()
        //    {
        //        Id = "c14748c1-a7e1-4d4c-8c28-b81148d28fe8",
        //        UserName = "WebAdmin",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "WebAdmin@gmail.com",
        //        FirstName = "Super",
        //        LastName = "Web",
        //        PhoneNumber = "00359888332211",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.WebsiteAdministratorRoleName,
        //    },
        //        new ApplicationUser()
        //    {
        //        Id = "23739b5e-7504-4e00-b7cc-8a2261f228c5",
        //        UserName = "Maid",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "Maid@gmail.com",
        //        FirstName = "Penka",
        //        LastName = "Chistata",
        //        PhoneNumber = "00359999333333",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.MaidRoleName,
        //    },
        //        new ApplicationUser()
        //    {
        //        Id = "64d6ea53-794b-4444-9cb5-75e3db678a1b",
        //        UserName = "Admin",
        //        PasswordHash = Guid.NewGuid().ToString(),
        //        Email = "Admin@gmail.com",
        //        FirstName = "TheBets",
        //        LastName = "Admin",
        //        PhoneNumber = "00359999000000",
        //        EmailConfirmed = true,
        //        Role = GlobalConstants.HotelManagerRoleName,
        //    },
        //    }.AsQueryable().AsAsyncEnumerable();

        //    await dbContext.UserRoles.AddAsync(
        //        new IdentityUserRole<string>()
        //        {
        //            // Hotel Manager Role
        //            RoleId = "2baec4cd-2b4c-491b-8679-a4a3e5bbd46b",
        //            UserId = "64d6ea53-794b-4444-9cb5-75e3db678a1b",
        //        });
        //    await dbContext.UserRoles.AddAsync(
        //        new IdentityUserRole<string>()
        //        {
        //            // Chef role
        //            RoleId = "3d3a64bc-f885-4f6b-be56-abf050a4a5ff",
        //            UserId = "585a155e-41c0-42b3-b4a2-acc0cd35408a",
        //        });
        //    await dbContext.UserRoles.AddAsync(
        //       new IdentityUserRole<string>()
        //       {
        //           // Waiter role
        //           RoleId = "f3521ccd-9d7c-4f62-8767-50d101f0ff90",
        //           UserId = "ff47ea75-3821-4c2d-8c87-168b074f8236",
        //       });
        //    await dbContext.UserRoles.AddAsync(
        //      new IdentityUserRole<string>()
        //      {
        //          // Maid role
        //          RoleId = "55359ebf-f641-4064-beca-8156aeedb42f",
        //          UserId = "23739b5e-7504-4e00-b7cc-8a2261f228c5",
        //      });
        //    await dbContext.UserRoles.AddAsync(
        //      new IdentityUserRole<string>()
        //      {
        //          // Receptionist role
        //          RoleId = "1159da12-1d8e-4a65-8701-c8be3f9c8ce2",
        //          UserId = "b671dd2b-f12c-48de-a523-1164bb799880",
        //      });
        //    await dbContext.UserRoles.AddAsync(
        //      new IdentityUserRole<string>()
        //      {
        //          // WebAdmin role
        //          RoleId = "374d7029-41e6-443a-a364-2118694f8a3e",
        //          UserId = "c14748c1-a7e1-4d4c-8c28-b81148d28fe8",
        //      });
        //    await dbContext.UserRoles.AddAsync(
        //      new IdentityUserRole<string>()
        //      {
        //          // Guest
        //          RoleId = "7ba79047-a04b-445b-a0c0-8efd401a7154",
        //          UserId = "9fabc808-d07d-44d5-9b23-6454705ddd48",
        //      });
        //    await dbContext.SaveChangesAsync();
        //    AutoMapperConfig.RegisterMappings(Assembly.Load("MyHotelWebsite.Web.ViewModels"));
        //    var staffService = new StaffService(usersRepoDB, mockUserManager.Object, mockRoleManager.Object, dbContext);
        //    //var inMemoryQueryable = new InMemoryAsyncQueryable<ApplicationRole>(roles);
        //    mockRoleManager.Setup(r => r.Roles).Returns(roles.GetAsyncEnumerator);
        //    mockUserManager.Setup(u => u.Users).Returns(users.GetAsyncEnumerator);

        //    IEnumerable<SingleStaffViewModel> employees = await staffService.GetAllEmployeesAsync(1, 3);
        //    Assert.NotEmpty(employees);
        //    Assert.Equal(6, employees.Count());
        //    dbContext.Dispose();
        //}

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
