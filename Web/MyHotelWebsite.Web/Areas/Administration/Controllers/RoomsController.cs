namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.MaidRoleName)]
    public class RoomsController : AdministrationController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
