namespace MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Dishes;

    public class ShoppingCartViewModel : IMapFrom<ShoppingCart>
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter a quantity between 1 and 1000")]
        public int Count { get; set; }

        [Required]
        public string DishId { get; set; }

        public virtual SingleDishViewModel Dish { get; set; }

        [NotMapped]
        public decimal Price { get; set; }
    }
}
