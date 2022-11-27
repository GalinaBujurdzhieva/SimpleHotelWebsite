using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyHotelWebsite.Data.Common.Repositories;
using MyHotelWebsite.Data.Models;
using MyHotelWebsite.Services.Mapping;
using MyHotelWebsite.Web.ViewModels.Administration.Blogs;
using MyHotelWebsite.Web.ViewModels.Blogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace MyHotelWebsite.Services.Data
{
    public class BlogsService : IBlogsService
    {
        private readonly IDeletableEntityRepository<Blog> blogsRepo;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public BlogsService(IDeletableEntityRepository<Blog> blogRepo)
        {
            this.blogsRepo = blogRepo;
        }

        public async Task AddBlogAsync(CreateBlogViewModel model, string staffId, string imagePath)
        {
            var blog = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                StaffId = staffId,
            };

            // Directory.CreateDirectory($"{imagePath}/blogs/");
            var blogImageExtension = Path.GetExtension(imagePath).TrimStart('.');

            if (this.allowedExtensions.Contains(blogImageExtension))
            {
                blog.BlogImageUrl = imagePath;
            }

            blog.BlogImage = new BlogImage
            {
                Extension = blogImageExtension,
                StaffId = staffId,
            };

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

        public Task EditBlogAsync(CreateBlogViewModel model, int id)
        {
            throw new NotImplementedException();
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
