using Microsoft.AspNetCore.Mvc;
using MyHotelWebsite.Services.Data;
using MyHotelWebsite.Web.ViewModels.Blogs;

namespace MyHotelWebsite.Web.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogsService blogService;
        public BlogsController(IBlogsService blogService)
        {
            this.blogService = blogService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All(int id)
        {
            var model = new BlogAllViewModel
            {
                Blogs = this.blogService.GetAllBlogs(id, 4),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
