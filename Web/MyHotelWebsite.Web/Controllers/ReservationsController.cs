using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class ReservationsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
