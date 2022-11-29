using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    public class DishesController : AdministrationController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
