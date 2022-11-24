using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyHotelWebsite.Data.Common.Repositories;
using MyHotelWebsite.Data.Models;
using MyHotelWebsite.Services.Mapping;
using MyHotelWebsite.Web.ViewModels.Blogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Services.Data
{
    public class BlogsService : IBlogsService
    {
        private readonly IDeletableEntityRepository<Blog> blogRepo;

        public BlogsService(IDeletableEntityRepository<Blog> blogRepo)
        {
            this.blogRepo = blogRepo;
        }

        public async Task<T> BlogDetailsByIdAsync<T>(int id)
        {
            var currentBlog = await this.blogRepo.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            return currentBlog;
        }

        public async Task<bool> DoesBlogExistsAsync(int id)
        {
            return await this.blogRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllBlogsAsync<T>(int page, int itemsPerPage = 4)
        {
            var blogs = await this.blogRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToListAsync();

            return blogs;
        }

        public async Task<IEnumerable<T>> GetLastBlogsAsync<T>(int count)
        {
            var lastBlogs = await this.blogRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Take(count).To<T>().ToListAsync();

            return lastBlogs;
        }
    }
}
