﻿namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.WebsiteAdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ChefRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.WaiterRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.MaidRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.ReceptionistRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.HotelAdministratorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
