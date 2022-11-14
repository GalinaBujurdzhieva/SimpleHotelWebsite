using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
