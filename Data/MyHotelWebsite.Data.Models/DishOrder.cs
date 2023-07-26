namespace MyHotelWebsite.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Common.Models;

    public class DishOrder : BaseDeletableModel<int>
    {
        [Required]
        public string DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int DishQuantity { get; set; }

        public decimal TotalPrice
            => this.DishQuantity * this.Dish.Price;
    }
}
