using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class RoomsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
