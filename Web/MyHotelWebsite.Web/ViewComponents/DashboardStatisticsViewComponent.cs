namespace MyHotelWebsite.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;
    using MyHotelWebsite.Web.ViewModels.ViewComponents;

    public class DashboardStatisticsViewComponent : ViewComponent
    {
        private readonly IBlogsService blogsService;
        private readonly IDishesService dishesService;
        private readonly IOrdersService ordersService;
        private readonly IRoomsService roomsService;
        private readonly IGuestsService guestsService;
        private readonly IReservationsService reservationsService;

        public DashboardStatisticsViewComponent(IBlogsService blogsService, IDishesService dishesService, IOrdersService ordersService, IRoomsService roomsService, IGuestsService guestsService, IReservationsService reservationsService)
        {
            this.blogsService = blogsService;
            this.dishesService = dishesService;
            this.ordersService = ordersService;
            this.roomsService = roomsService;
            this.reservationsService = reservationsService;
            this.guestsService = guestsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var freeRooms = await this.roomsService.GetAllFreeRoomsAtTheMomentAsync<SingleRoomViewModel>();
            var allRoomsCount = await this.roomsService.GetCountAsync();

            var viewModel = new DashboardStatisticsViewModel
            {
                BlogsCount = await this.blogsService.GetCountAsync(),
                DishesCount = await this.dishesService.GetCountAsync(),
                OrdersCount = await this.ordersService.GetCountAsync(),
                OccupiedRoomsCount = allRoomsCount - freeRooms.Count(),
                ReservationsCount = await this.reservationsService.GetCountAsync(),
                GuestsCount = await this.guestsService.GetCountAsync(),
            };
            return this.View(viewModel);
        }
    }
}
