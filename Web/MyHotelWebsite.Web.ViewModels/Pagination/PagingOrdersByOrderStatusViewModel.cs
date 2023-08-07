namespace MyHotelWebsite.Web.ViewModels.Pagination
{
    using MyHotelWebsite.Data.Models.Enums;

    public class PagingOrdersByOrderStatusViewModel : PagingAllViewModel
    {
        public OrderStatus OrderStatus { get; set; }
    }
}
