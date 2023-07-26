namespace MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts
{
    using System.Collections.Generic;

    public class AllShoppingCartsOfOneUserViewModel
    {
        public List<SingleShoppingCartViewModel> ShoppingCartsList { get; set; } = new List<SingleShoppingCartViewModel>();

        public decimal TotalPrice { get; set; }

        public int Count => this.ShoppingCartsList.Count;
    }
}
