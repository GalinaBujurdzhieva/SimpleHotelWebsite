namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Web.ViewModels.User;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using MyHotelWebsite.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using MyHotelWebsite.Common;
    using System.Data;

    [Authorize(Roles = GlobalConstants.HotelAdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
