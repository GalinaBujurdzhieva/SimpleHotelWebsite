namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
