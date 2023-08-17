namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> guestSeeder = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (guestSeeder.Users.Any(u => u.Email == "testGuest1@gmail.com"))
            {
                return;
            }

            var resultOne = await guestSeeder.CreateAsync(
                new ApplicationUser
                {
                    Id = "e7522d06-694d-403b-86c7-175020363add",
                    UserName = "TestGuest1",
                    Email = "testGuest1@gmail.com",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    PhoneNumber = "00359888112233",
                    EmailConfirmed = true,
                },
                "TestGuest1");

            if (resultOne.Succeeded)
            {
                var user = await guestSeeder.FindByNameAsync("TestGuest1");
                await guestSeeder.AddToRoleAsync(user, GlobalConstants.GuestRoleName);
                dbContext.SaveChanges();
            }

            if (guestSeeder.Users.Any(u => u.Email == "testGuest2@gmail.com"))
            {
                return;
            }

            var resultTwo = await guestSeeder.CreateAsync(
                new ApplicationUser
                {
                    Id = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
                    UserName = "TestGuest2",
                    Email = "testGuest2@gmail.com",
                    FirstName = "Petar",
                    LastName = "Petrov",
                    PhoneNumber = "00359999332211",
                    EmailConfirmed = true,
                },
                "TestGuest2");

            if (resultTwo.Succeeded)
            {
                var user = await guestSeeder.FindByNameAsync("TestGuest2");
                await guestSeeder.AddToRoleAsync(user, GlobalConstants.GuestRoleName);
                dbContext.SaveChanges();
            }
        }
    }
}
