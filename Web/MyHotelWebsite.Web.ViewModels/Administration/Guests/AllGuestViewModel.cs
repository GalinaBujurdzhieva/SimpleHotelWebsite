namespace MyHotelWebsite.Web.ViewModels.Administration.Guests
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class AllGuestViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleGuestViewModel> Guests { get; set; } = new List<SingleGuestViewModel>();
    }
}
