using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class OrdersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
