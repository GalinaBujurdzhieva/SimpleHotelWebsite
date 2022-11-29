namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;
    using MyHotelWebsite.Web.ViewModels.Blogs;

    public class BlogsController : AdministrationController
    {
        private readonly IBlogsService blogService;
        private readonly IWebHostEnvironment environment;

        public BlogsController(IBlogsService blogService, IWebHostEnvironment environment)
        {
            this.blogService = blogService;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            var model = new CreateBlogViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.blogService.AddBlogAsync(model, staffId, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Could not add this blog");
                return this.View(model);
            }

            this.TempData["Message"] = "Blog added successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int BlogsPerPage = 6;
            var model = new BlogAllViewModel
            {
                ItemsPerPage = BlogsPerPage,
                AllEntitiesCount = await this.blogService.GetCountAsync(),
                Blogs = await this.blogService.GetAllBlogsAsync<SingleBlogViewModel>(id, BlogsPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.blogService.DoesBlogExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.blogService.BlogDetailsByIdAsync<SingleBlogViewModel>(id);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.blogService.DoesBlogExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.blogService.BlogDetailsByIdAsync<EditBlogViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBlogViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.blogService.EditBlogAsync(model, id, staffId, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Could not edit this blog");
                return this.View(model);
            }

            this.TempData["Message"] = "Blog edited successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.blogService.DoesBlogExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.blogService.DeleteBlogAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
