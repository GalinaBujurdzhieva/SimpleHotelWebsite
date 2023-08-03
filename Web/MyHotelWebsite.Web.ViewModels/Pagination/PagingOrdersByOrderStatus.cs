namespace MyHotelWebsite.Web.ViewModels.Pagination
{
    using MyHotelWebsite.Data.Models.Enums;

    public class PagingOrdersByOrderStatus : PagingAllViewModel
    {
        public OrderStatus OrderStatus { get; set; }
    }
}
