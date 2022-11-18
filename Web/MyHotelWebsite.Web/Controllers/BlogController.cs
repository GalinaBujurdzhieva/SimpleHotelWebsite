using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class BlogController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
