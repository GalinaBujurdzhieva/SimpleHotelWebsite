namespace MyHotelWebsite.Web.ViewModels.Administration.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class HotelAdministrationChangeStatusOfOrderViewModel
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
