using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyHotelWebsite.Data.Models;
using MyHotelWebsite.Services.Data;
using MyHotelWebsite.Web.ViewModels.Administration.Blogs;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
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
            return this.RedirectToAction("All", "Blogs", new { area = " " });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.blogService.DeleteBlogAsync(id);
            return this.RedirectToAction("All", "Blogs", new { area = " " });
        }
    }
}
