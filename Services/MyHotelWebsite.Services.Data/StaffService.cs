namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Staff;

    public class StaffService : IStaffService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> staffRepo;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public StaffService(IDeletableEntityRepository<ApplicationUser> staffRepo, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext)
        {
                this.userManager = userManager;
                this.staffRepo = staffRepo;
                this.roleManager = roleManager;
                this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SingleStaffViewModel>> GetAllEmployeesAsync(int page, int itemsPerPage = 4)
        {
            var roles = await this.roleManager.Roles.ToListAsync();
            var userRoles = await this.dbContext.UserRoles.ToListAsync();
            var users = await this.userManager.Users.ToListAsync();
            var guestRoleId = roles.Where(r => r.Name == GlobalConstants.GuestRoleName).FirstOrDefault().Id;

            foreach (var employee in users)
            {
                var roleId = userRoles.FirstOrDefault(ur => ur.UserId == employee.Id).RoleId;
                employee.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
            }

            var allStaffList = users
               .Where(u => u.Roles.Any(r => !(r.RoleId == guestRoleId)))
               .OrderBy(u => u.FirstName)
               .ThenBy(u => u.LastName)
               .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).Select(u => new SingleStaffViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Role = u.Role,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                });

            return allStaffList;
        }

        public async Task<int> GetCountAsync()
        {
            var webAdministrators = await this.userManager.GetUsersInRoleAsync(GlobalConstants.WebsiteAdministratorRoleName);
            var receptionists = await this.userManager.GetUsersInRoleAsync(GlobalConstants.ReceptionistRoleName);
            var chefs = await this.userManager.GetUsersInRoleAsync(GlobalConstants.ChefRoleName);
            var maids = await this.userManager.GetUsersInRoleAsync(GlobalConstants.MaidRoleName);
            var waiters = await this.userManager.GetUsersInRoleAsync(GlobalConstants.WaiterRoleName);
            var manager = await this.userManager.GetUsersInRoleAsync(GlobalConstants.HotelManagerRoleName);

            return webAdministrators.Count() + receptionists.Count() + chefs.Count() + maids.Count() + waiters.Count() + manager.Count;
        }

        public async Task<int> GetCountOfEmployeesByRoleAsync(string role)
        {
            var roles = await this.roleManager.Roles.ToListAsync();
            var userRoles = await this.dbContext.UserRoles.ToListAsync();
            var users = await this.userManager.Users.ToListAsync();

            foreach (var employee in users)
            {
                var roleId = userRoles.FirstOrDefault(ur => ur.UserId == employee.Id).RoleId;
                employee.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
            }

            if (!string.IsNullOrEmpty(role))
            {
                users = users
                    .Where(x => x.Role == role).ToList();
            }

            return users.Count();
        }

        public async Task<IEnumerable<SingleStaffViewModel>> GetEmployeesByRoleAsync(int page, string role, int itemsPerPage = 4)
        {
            var roles = await this.roleManager.Roles.ToListAsync();
            var userRoles = await this.dbContext.UserRoles.ToListAsync();
            var users = await this.userManager.Users.ToListAsync();

            foreach (var employee in users)
            {
                var roleId = userRoles.FirstOrDefault(ur => ur.UserId == employee.Id).RoleId;
                employee.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
            }

            if (!string.IsNullOrEmpty(role))
            {
                users = users
                    .Where(x => x.Role == role).ToList();
            }

            return users.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).Select(u => new SingleStaffViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Role = u.Role,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
            }).ToList();
        }

        public async Task LockUser(string id)
        {
            ApplicationUser userToBeLocked = await this.userManager.FindByIdAsync(id);
            if (userToBeLocked != null)
            {
                if (userToBeLocked.LockoutEnd == null || userToBeLocked.LockoutEnd < DateTime.UtcNow)
                {
                    userToBeLocked.LockoutEnd = DateTime.UtcNow.AddYears(1000);
                    await this.staffRepo.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task UnlockUser(string id)
        {
            ApplicationUser userToBeLocked = await this.userManager.FindByIdAsync(id);
            if (userToBeLocked != null)
            {
                if (userToBeLocked.LockoutEnd > DateTime.UtcNow)
                {
                    userToBeLocked.LockoutEnd = DateTime.UtcNow;
                    await this.staffRepo.SaveChangesAsync();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
