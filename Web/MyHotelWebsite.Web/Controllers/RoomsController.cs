namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class RoomsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
