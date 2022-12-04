namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Web.Controllers;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
