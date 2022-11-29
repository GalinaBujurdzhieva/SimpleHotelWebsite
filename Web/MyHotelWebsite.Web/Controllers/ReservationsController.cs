namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReservationsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
