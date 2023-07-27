namespace MyHotelWebsite.Web.ViewModels.Guests.Orders
{
    using System;
    using System.Collections.Generic;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class SingleOrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<SingleDishOrderViewModel> DishOrders { get; set; } = new List<SingleDishOrderViewModel>();
    }
}
