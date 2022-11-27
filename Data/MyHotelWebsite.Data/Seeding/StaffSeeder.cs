//using Microsoft.EntityFrameworkCore;
//using MyHotelWebsite.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyHotelWebsite.Data.Seeding
//{
//    internal class StaffSeeder : ISeeder
//    {
//        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
//        {
//            if (dbContext.UserRoles.Any())
//            {
//                return;
//            }

//            await dbContext.UserRoles
//                .AddAsync(new { u });
//        }
//    }
//}
