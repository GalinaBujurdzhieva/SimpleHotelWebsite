namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;

    internal class BlogsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await dbContext.Blogs.AddAsync(new Blog
            {
                Title = "Our newly equipped \"power place\" Gym with the latest fitness equipment",
                Content = "In our newly equipped fitness room you can get your circulation going, strengthen your muscles and do something good for your body. With high-quality and professional fitness equipment from Technogym, we offer you an extended range of sports. Endurance machines that are suitable for both light endurance training or a warm-up, as well as for a sweat-inducing and strengthening cardio workout. On the treadmill you can do a running workout that is easy on your joints, and the elliptical trainer provides a varied and effective whole-body workout. With the Concept 2 rowing machine and a bicycle, we provide you with professional indoor fitness equipment for the highest fitness demands. During your workout, you can connect to your Bluetooth headphones, on the internet-enabled equipment, and listen to your favourite music, watch a film or put together your individual workout with the free mobile phone app from Technogym.\r\n\r\nWith the strength station, you professionally train the muscles throughout your body. Various exercises as well as a wall bars and extensive accessories define and tone the body and make fitness hearts beat faster. A dumbbell rack and the matching dumbbell set guarantee professional dumbbell and strength training. A dumbbell bar is the ideal addition to our training station and perfects the gym equipment.\r\n\r\nA fascia set and a yoga mat are available for the perfect finish to release tension and loosen up the body again after your workout. ",
                BlogImageUrl = "wwwroot\\images\\blogs.1.png",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
