﻿using Microsoft.AspNetCore.Mvc;
using MyHotelWebsite.Services.Data;
using MyHotelWebsite.Web.ViewModels.ViewComponents;
using System.Threading.Tasks;

namespace MyHotelWebsite.Web.ViewComponents
{
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
            var viewModel = new DashboardStatisticsViewModel
            {
                BlogsCount = await this.blogsService.GetCountAsync(),
                DishesCount = await this.dishesService.GetCountAsync(),
                OrdersCount = await this.ordersService.GetCountAsync(),
                RoomsCount = await this.roomsService.GetCountAsync(),
                ReservationsCount = await this.reservationsService.GetCountAsync(),
                GuestsCount = await this.guestsService.GetCountAsync(),
            };
            return this.View(viewModel);
        }
    }
}
