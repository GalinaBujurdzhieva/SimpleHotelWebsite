namespace MyHotelWebsite.Web.ViewModels.Administration.Orders
{
    using System;
    using System.Collections.Generic;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;

    public class HotelAdministrationSingleOrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<SingleDishOrderViewModel> DishOrders { get; set; } = new List<SingleDishOrderViewModel>();
    }
}
