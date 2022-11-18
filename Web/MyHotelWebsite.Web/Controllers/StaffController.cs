namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class StaffController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
