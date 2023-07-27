namespace MyHotelWebsite.Web.Areas.Guest.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.Controllers;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;

    [Area("Guest")]
    [Authorize(Roles = GlobalConstants.GuestRoleName)]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new AllDishesInOneOrderViewModel
            {
                Dishes = await this.ordersService.GetOrderDetailsAsync<SingleDishOrderViewModel>(id),
            };
            model.TotalPrice = this.ordersService.GetOrderTotalAsync(model.Dishes);
            return this.View(model);
        }

        public async Task<IActionResult> MyOrders(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int OrdersPerPage = 10;
            var applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new AllOrderViewModel
            {
                ItemsPerPage = OrdersPerPage,
                AllEntitiesCount = await this.ordersService.GetCountOfMyOrdersAsync(applicationUserId),
                Orders = await this.ordersService.GetMyOrdersAsync<SingleOrderViewModel>(applicationUserId, id, OrdersPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
