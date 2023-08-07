namespace MyHotelWebsite.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class HotelAdministrationOrderByOrderStatusViewModel : PagingOrdersByOrderStatusViewModel
    {
        public IEnumerable<HotelAdministrationSingleOrderViewModel> Orders { get; set; } = new List<HotelAdministrationSingleOrderViewModel>();
    }
}
