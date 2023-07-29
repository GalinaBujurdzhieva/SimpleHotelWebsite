namespace MyHotelWebsite.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class HotelAdministrationAllOrderViewModel : PagingAllViewModel
    {
        public IEnumerable<HotelAdministrationSingleOrderViewModel> Orders { get; set; } = new List<HotelAdministrationSingleOrderViewModel>();
    }
}
