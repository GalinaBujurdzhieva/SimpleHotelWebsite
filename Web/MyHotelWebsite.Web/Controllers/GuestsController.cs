using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class GuestsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
