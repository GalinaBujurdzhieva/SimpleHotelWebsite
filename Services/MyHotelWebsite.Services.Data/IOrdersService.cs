namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;
    using Syncfusion.Pdf;

    public interface IOrdersService
    {
        Task AddOrderAsync(AllShoppingCartsOfOneUserViewModel model, string applicationUserId);

        Task DeleteOrderAsync(int id);

        Task<bool> DoesOrderExistsAsync(int id);

        Task<PdfDocument> FillPdfOrderAsync(int id);

        Task<List<object>> FillPdfTableWithDishes(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountOfMyOrdersAsync(string applicationUserId);

        Task<IEnumerable<T>> GetMyOrdersAsync<T>(string applicationUserId, int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetOrderDetailsAsync<T>(int id);

        decimal GetOrderTotalAsync(IEnumerable<SingleDishOrderViewModel> dishesList);

        Task<IEnumerable<T>> HotelAdministrationGetAllOrdersAsync<T>(int page, int itemsPerPage = 4);
    }
}
