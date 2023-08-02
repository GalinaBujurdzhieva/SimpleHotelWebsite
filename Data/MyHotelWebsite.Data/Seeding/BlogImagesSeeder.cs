namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;

    internal class BlogImagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BlogImages.Any())
            {
                return;
            }

            var allBlogsList = dbContext.Blogs.ToList();
            var allBlogImagesList = new List<BlogImage>();

            foreach (var blog in allBlogsList)
            {
                var currentBlogImageExtension = blog.BlogImageUrl.Split('.').Last();
                var blogImage = new BlogImage
                {
                    BlogId = blog.Id,
                    Extension = currentBlogImageExtension,
                };
                allBlogImagesList.Add(blogImage);
            }

            await dbContext.BlogImages.AddRangeAsync(allBlogImagesList);
            await dbContext.SaveChangesAsync();
        }
    }
}
