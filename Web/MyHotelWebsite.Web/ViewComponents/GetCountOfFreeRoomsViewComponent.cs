namespace MyHotelWebsite.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;
    using MyHotelWebsite.Web.ViewModels.ViewComponents;

    public class GetCountOfFreeRoomsViewComponent : ViewComponent
    {
        private readonly IRoomsService roomsService;

        public GetCountOfFreeRoomsViewComponent(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            await this.roomsService.LeaveOccupiedRoomsAsync();
            var freeRooms = await this.roomsService.GetAllFreeRoomsAtTheMomentAsync<SingleRoomViewModel>();

            var viewModel = new FreeRoomsAtTheMomentViewModel
            {
                FreeRoomsCount = freeRooms.Count(),
            };
            return this.View(viewModel);
        }
    }
}
