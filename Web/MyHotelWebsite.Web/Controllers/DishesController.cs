using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class DishesController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}
