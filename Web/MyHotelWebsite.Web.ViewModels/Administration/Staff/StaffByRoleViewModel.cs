namespace MyHotelWebsite.Web.ViewModels.Administration.Staff
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class StaffByRoleViewModel : PagingStaffByRoleViewModel
    {
        public IEnumerable<SingleStaffViewModel> Staff { get; set; } = new List<SingleStaffViewModel>();
    }
}
