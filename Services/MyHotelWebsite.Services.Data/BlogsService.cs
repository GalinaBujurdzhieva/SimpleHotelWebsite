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

        public BlogsService(IDeletableEntityRepository<Blog> blogRepo, IHostingEnvironment hostingEnvironment)
        {
            this.blogRepo = blogRepo;
        }

        public IEnumerable<T> GetAllBlogs<T>(int page, int itemsPerPage = 4)
        {
            var blogs = this.blogRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToList();

            return blogs;
        }

        public IEnumerable<T> GetLastBlogs<T>(int count)
        {
            var lastBlogs = this.blogRepo.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Take(count).To<T>().ToList();

            return lastBlogs;
        }
    }
}
