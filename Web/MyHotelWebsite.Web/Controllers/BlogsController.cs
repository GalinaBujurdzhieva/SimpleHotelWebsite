namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Blogs;
    using System.IO;
    using System.Threading.Tasks;

    public class BlogsController : BaseController
    {
        private readonly IBlogsService blogService;

        public BlogsController(IBlogsService blogService)
        {
            this.blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new BlogAllViewModel
            {
                Blogs = await this.blogService.GetLastBlogsAsync<SingleBlogViewModel>(2),
            };
            return this.View(model);
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int BlogsPerPage = 4;
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
    }
}
