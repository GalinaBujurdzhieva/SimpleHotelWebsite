using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
