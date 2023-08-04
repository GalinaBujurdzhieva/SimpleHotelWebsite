namespace MyHotelWebsite.Web.ViewModels.Administration.Staff
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class AllStaffViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleStaffViewModel> Staff { get; set; } = new List<SingleStaffViewModel>();
    }
}
