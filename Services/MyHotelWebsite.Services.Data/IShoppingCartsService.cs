namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task AddDishInTheShoppingCartAsync(SingleShoppingCartViewModel shoppingCart);

        Task DecreaseQuantityOfTheDishInTheShoppingCart(int shoppingCartId);

        Task<List<SingleShoppingCartViewModel>> GetAllSingleShoppingCartsOfTheUserAsync(string applicationUserId);

        decimal GetOrderTotalOfShoppingCartsOfTheUser(IEnumerable<SingleShoppingCartViewModel> shoppingCartsList);

        Task IncreaseQuantityOfTheDishInTheShoppingCart(int shoppingCartId);

        Task<bool> IsDishAlreadyInTheShoppingCartOfThatUserAsync(string dishId, string applicationUserId);

        Task RemoveDishFromTheShoppingCart(int shoppingCartId);

        Task UpdateDishCountInTheShoppingCartAsync(string dishId, string applicationUserId, int count);
    }
}
