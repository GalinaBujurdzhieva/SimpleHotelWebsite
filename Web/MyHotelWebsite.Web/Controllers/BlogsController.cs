namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Blogs;
    using System.IO;

    public class BlogsController : BaseController
    {
        private readonly IBlogsService blogService;

        public BlogsController(IBlogsService blogService, IWebHostEnvironment hostingEnvironment)
        {
            this.blogService = blogService;
        }

        public IActionResult Index()
        {
            var model = new BlogAllViewModel
            {
                Blogs = this.blogService.GetLastBlogs<SingleBlogViewModel>(2),
            };
            return this.View(model);
        }

        public IActionResult All(int id = 1)
        {
            var model = new BlogAllViewModel
            {
                Blogs = this.blogService.GetAllBlogs<SingleBlogViewModel>(id, 4),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
