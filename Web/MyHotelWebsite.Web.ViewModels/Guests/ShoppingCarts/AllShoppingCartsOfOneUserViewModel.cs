namespace MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts
{
    using System.Collections.Generic;

    public class AllShoppingCartsOfOneUserViewModel
    {
        public IEnumerable<SingleShoppingCartViewModel> ShoppingCartsList { get; set; } = new List<SingleShoppingCartViewModel>();

        public decimal TotalPrice { get; set; }
    }
}
