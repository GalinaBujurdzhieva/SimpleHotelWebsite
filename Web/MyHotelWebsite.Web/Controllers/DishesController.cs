using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class DishesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
