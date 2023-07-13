namespace MyHotelWebsite.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Web.ViewModels;

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

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return this.View(
        //        new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        //}
    }
}
