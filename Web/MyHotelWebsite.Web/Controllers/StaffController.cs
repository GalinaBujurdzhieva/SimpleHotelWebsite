namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
