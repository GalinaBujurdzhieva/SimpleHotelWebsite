namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;

    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> adminSeeder = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (adminSeeder.Users.Any(u => u.Email == "gal.vins301184@gmail.com"))
            {
                return;
            }

            var result = await adminSeeder.CreateAsync(
                new ApplicationUser
            {
                UserName = "Admin",
                Email = "gal.vins301184@gmail.com",
                FirstName = "Galina",
                LastName = "Stoyanova",
                EmailConfirmed = true,
            },
                "Dariq200108");

            if (result.Succeeded)
            {
                var user = await adminSeeder.FindByNameAsync("Admin");
                await adminSeeder.AddToRoleAsync(user, GlobalConstants.HotelAdministratorRoleName);
                dbContext.SaveChanges();
            }
        }
    }
}
