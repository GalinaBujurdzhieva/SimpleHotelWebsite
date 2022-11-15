using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class GuestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
