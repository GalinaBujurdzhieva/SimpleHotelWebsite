namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task<bool> IsDishAlreadyInTheShoppingCartOfThatUserAsync(string dishId, string applicationUserId);

        Task AddDishInTheShoppingCartAsync(ShoppingCartViewModel shoppingCart);

        Task UpdateDishCountInTheShoppingCartAsync(string dishId, string applicationUserId, int count);
    }
}
