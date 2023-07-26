namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public interface IOrdersService
    {
        Task AddOrderAsync(AllShoppingCartsOfOneUserViewModel model, string applicationUserId);

        Task<int> GetCountAsync();
    }
}
