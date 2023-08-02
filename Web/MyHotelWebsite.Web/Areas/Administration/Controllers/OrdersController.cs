﻿namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
    using Syncfusion.Pdf;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.ChefRoleName + ", " + GlobalConstants.WaiterRoleName)]
    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
                this.ordersService = ordersService;
        }

        public async Task<IActionResult> AddComment(int id)
        {
            if (!await this.ordersService.DoesOrderExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            HotelAdministrationAddCommentToOrderViewModel model = await this.ordersService.GetOrderDetailsToAddCommentAsync(id);
            return this.View(model);
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int OrdersPerPage = 10;

            var model = new HotelAdministrationAllOrderViewModel
            {
                ItemsPerPage = OrdersPerPage,
                AllEntitiesCount = await this.ordersService.GetCountAsync(),
                Orders = await this.ordersService.HotelAdministrationGetAllOrdersAsync<HotelAdministrationSingleOrderViewModel>(id, OrdersPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }

        public async Task<IActionResult> CreatePdfDocument(int id)
        {
            PdfDocument document = await this.ordersService.FillPdfOrderAsync(id);
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            document.Close(true);
            string contentType = "application/pdf";
            string fileName = $"Order {id}.pdf";
            return this.File(stream, contentType, fileName);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.ordersService.DoesOrderExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.ordersService.DeleteOrderAsync(id);
            return this.RedirectToAction(nameof(this.All));
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
    }
}
