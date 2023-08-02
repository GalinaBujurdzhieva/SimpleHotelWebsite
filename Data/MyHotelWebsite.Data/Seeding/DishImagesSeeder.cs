namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;

    internal class DishImagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DishImages.Any())
            {
                return;
            }

            var allDishesList = dbContext.Dishes.ToList();
            var allDishImagesList = new List<DishImage>();

            foreach (var dish in allDishesList)
            {
                var currentDishImageExtension = dish.DishImageUrl.Split('.').Last();
                var dishImage = new DishImage
                {
                    DishId = dish.Id,
                    Extension = currentDishImageExtension,
                };
                allDishImagesList.Add(dishImage);
            }

            await dbContext.DishImages.AddRangeAsync(allDishImagesList);
            await dbContext.SaveChangesAsync();
        }
    }
}
