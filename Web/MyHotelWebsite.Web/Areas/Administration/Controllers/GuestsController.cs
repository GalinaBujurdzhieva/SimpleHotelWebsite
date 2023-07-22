namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.ReceptionistRoleName + ", " + GlobalConstants.WebsiteAdministratorRoleName)]
    public class GuestsController : AdministrationController
    {
        private readonly IGuestsService guestsService;

        public GuestsController(IGuestsService guestsService)
        {
            this.guestsService = guestsService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int GuestsPerPage = 10;

            var model = new AllGuestViewModel
            {
                ItemsPerPage = GuestsPerPage,
                AllEntitiesCount = await this.guestsService.GetCountAsync(),
                Guests = await this.guestsService.GetAllGuestsAsync<SingleGuestViewModel>(id, GuestsPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
