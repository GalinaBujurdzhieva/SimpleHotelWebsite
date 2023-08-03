namespace MyHotelWebsite.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Common.Models;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public string DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public bool IsShoppingCartAddedToAFinalOrder { get; set; } = false;
    }
}
