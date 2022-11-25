namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Web.ViewModels.User;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using MyHotelWebsite.Data.Models;

    [Authorize(Roles = GlobalConstants.WebsiteAdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
