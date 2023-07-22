namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult AccessDenied()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        public IActionResult Gallery()
        {
            return this.View();
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
