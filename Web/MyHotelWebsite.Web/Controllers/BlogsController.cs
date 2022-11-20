using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class BlogsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
