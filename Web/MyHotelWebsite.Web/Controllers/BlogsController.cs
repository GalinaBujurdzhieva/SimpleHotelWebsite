﻿namespace MyHotelWebsite.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Blogs;

    public class BlogsController : BaseController
    {
        private readonly IBlogsService blogService;

        public BlogsController(IBlogsService blogService)
        {
            this.blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AllBlogViewModel
            {
                Blogs = await this.blogService.GetLastBlogsAsync<SingleBlogViewModel>(2),
            };

            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(model);
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int BlogsPerPage = 4;
            var model = new AllBlogViewModel
            {
                ItemsPerPage = BlogsPerPage,
                AllEntitiesCount = await this.blogService.GetCountAsync(),
                Blogs = await this.blogService.GetAllBlogsAsync<SingleBlogViewModel>(id, BlogsPerPage),
                PageNumber = id,
            };
            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.blogService.DoesBlogExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.blogService.BlogDetailsByIdAsync<SingleBlogViewModel>(id);
            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(model);
        }
    }
}
