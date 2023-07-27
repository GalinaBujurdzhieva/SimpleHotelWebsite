namespace MyHotelWebsite.Web.ViewModels.Guests.Orders
{
    using System.Collections.Generic;

    public class AllDishesInOneOrderViewModel
    {
        public IEnumerable<SingleDishOrderViewModel> Dishes { get; set; } = new List<SingleDishOrderViewModel>();

        public decimal TotalPrice { get; set; }
    }
}
