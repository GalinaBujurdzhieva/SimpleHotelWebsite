using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class ReservationsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
