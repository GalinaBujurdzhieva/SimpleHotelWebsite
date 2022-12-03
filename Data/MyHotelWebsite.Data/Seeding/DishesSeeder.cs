namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class DishesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Dishes.Any())
            {
                return;
            }

            List<Dish> dishes = new List<Dish>();

            // 1
            dishes.Add(new Dish
            {
                Name = "Coffee Espresso",
                Price = 2.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 50,
                DishImageUrl = "images/dishes/hotDrinks/1.png",
            });

            // 2
            dishes.Add(new Dish
            {
                Name = "Coffee Lavazza",
                Price = 3.00M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 30,
                DishImageUrl = "images/dishes/hotDrinks/2.png",
            });

            // 3
            dishes.Add(new Dish
            {
                Name = "Cappucino",
                Price = 3.50M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 20,
                DishImageUrl = "images/dishes/hotDrinks/3.png",
            });

            // 4
            dishes.Add(new Dish
            {
                Name = "Vienna coffee",
                Price = 3.30M,
                DishCategory = DishCategory.HotDrinks,
                QuantityInStock = 25,
                DishImageUrl = "images/dishes/hotDrinks/4.png",
            });

            await dbContext.Dishes.AddRangeAsync(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
