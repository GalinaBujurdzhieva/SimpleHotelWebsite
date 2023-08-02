namespace MyHotelWebsite.Web.ViewModels.Administration.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class HotelAdministrationAddCommentToOrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        [MaxLength(1000, ErrorMessage = "{0} can't be more than {1} characters.")]
        public string Comment { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
