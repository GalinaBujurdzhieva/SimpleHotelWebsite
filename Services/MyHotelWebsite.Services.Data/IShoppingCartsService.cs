namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public interface IShoppingCartsService
    {
     Task AddDishInTheShoppingCartAsync(SingleShoppingCartViewModel shoppingCart);

     Task<IEnumerable<T>> GetAllSingleShoppingCartsOfTheUser<T>(string applicationUserId);

     Task<bool> IsDishAlreadyInTheShoppingCartOfThatUserAsync(string dishId, string applicationUserId);

     Task UpdateDishCountInTheShoppingCartAsync(string dishId, string applicationUserId, int count);
    }
}
