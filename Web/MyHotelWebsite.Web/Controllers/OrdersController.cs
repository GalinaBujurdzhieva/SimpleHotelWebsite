using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
