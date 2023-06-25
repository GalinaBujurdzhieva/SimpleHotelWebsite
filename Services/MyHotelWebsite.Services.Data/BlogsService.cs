namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;

    public class BlogsService : IBlogsService
    {
        private readonly IDeletableEntityRepository<Blog> blogsRepo;
        private readonly IDeletableEntityRepository<BlogImage> blogImagesRepo;

        public BlogsService(IDeletableEntityRepository<Blog> blogRepo, IDeletableEntityRepository<BlogImage> blogImagesRepo)
        {
            this.blogsRepo = blogRepo;
            this.blogImagesRepo = blogImagesRepo;
        }

        public async Task AddBlogAsync(CreateBlogViewModel model, string applicationUserId, string imagePath)
        {
            var blog = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                ApplicationUserId = applicationUserId,
            };

            Directory.CreateDirectory($"{imagePath}/blogs/");
            var blogImageExtension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var blogImage = new BlogImage
            {
                Extension = blogImageExtension,
                ApplicationUserId = applicationUserId,
            };

            blog.BlogImage = blogImage;
            blog.BlogImageId = blogImage.Id;
            blog.BlogImageUrl = $"images/blogs/{blogImage.Id}.{blogImageExtension}";
            var physicalPath = $"{imagePath}/blogs/{blogImage.Id}.{blogImageExtension}";
            using FileStream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this.blogsRepo.AddAsync(blog);
            await this.blogsRepo.SaveChangesAsync();
        }

        public async Task<T> BlogDetailsByIdAsync<T>(int id)
        {
            var currentBlog = await this.blogsRepo.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            return currentBlog;
        }

        public async Task DeleteBlogAsync(int id)
        {
            var currentBlog = await this.blogsRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            this.blogsRepo.Delete(currentBlog);
            await this.blogsRepo.SaveChangesAsync();
        }

        public async Task<bool> DoesBlogExistsAsync(int id)
        {
            return await this.blogsRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task EditBlogAsync(EditBlogViewModel model, int id, string applicationUserId, string imagePath)
        {
            var currentBlog = await this.blogsRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            currentBlog.Title = model.Title;
            currentBlog.Content = model.Content;

            var currentBlogImage = await this.blogImagesRepo.All().FirstOrDefaultAsync(x => x.BlogId == currentBlog.Id);
            if (currentBlogImage != null)
            {
                this.blogImagesRepo.HardDelete(currentBlogImage);
            }

            currentBlog.ApplicationUserId = applicationUserId;

            Directory.CreateDirectory($"{imagePath}/blogs/");
            var blogImageExtension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var blogImage = new BlogImage
            {
                Extension = blogImageExtension,
                ApplicationUserId = applicationUserId,
            };
            currentBlog.BlogImage = blogImage;
            currentBlog.BlogImageId = blogImage.Id;
            currentBlog.BlogImageUrl = $"images/blogs/{blogImage.Id}.{blogImageExtension}";
            var physicalPath = $"{imagePath}/blogs/{blogImage.Id}.{blogImageExtension}";
            using FileStream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);
            await this.blogsRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllBlogsAsync<T>(int page, int itemsPerPage = 4)
        {
            var blogs = await this.blogsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();
            return blogs;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.blogsRepo.AllAsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<T>> GetLastBlogsAsync<T>(int count)
        {
            var lastBlogs = await this.blogsRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Take(count).To<T>().ToListAsync();

            return lastBlogs;
        }
    }
}
