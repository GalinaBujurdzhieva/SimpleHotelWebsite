namespace MyHotelWebsite.Web.ViewModels.Guests.Orders
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class AllOrderViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleOrderViewModel> Orders { get; set; } = new List<SingleOrderViewModel>();
    }
}
