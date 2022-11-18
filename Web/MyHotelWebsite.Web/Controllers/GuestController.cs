using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class GuestController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
