namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.MaidRoleName)]
    public class RoomsController : AdministrationController
    {
        private readonly IRoomsService roomsService;

        public RoomsController(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int RoomsPerPage = 12;
            var model = new RoomAllViewModel
            {
                ItemsPerPage = RoomsPerPage,
                AllEntitiesCount = await this.roomsService.GetCountAsync(),
                Rooms = await this.roomsService.GetAllRoomsAsync<SingleRoomViewModel>(id, RoomsPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
