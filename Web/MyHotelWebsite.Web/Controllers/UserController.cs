using Microsoft.AspNetCore.Mvc;

namespace MyHotelWebsite.Web.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
