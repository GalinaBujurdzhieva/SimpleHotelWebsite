namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.WebsiteAdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
