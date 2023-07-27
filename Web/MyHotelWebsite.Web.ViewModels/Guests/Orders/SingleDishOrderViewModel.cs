namespace MyHotelWebsite.Web.ViewModels.Guests.Orders
{
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;

    public class SingleDishOrderViewModel : IMapFrom<DishOrder>
    {
        public int Id { get; set; }

        public string DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int DishQuantity { get; set; }

        public decimal TotalPrice
            => this.DishQuantity * this.Dish.Price;
    }
}
