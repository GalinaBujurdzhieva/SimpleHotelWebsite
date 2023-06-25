namespace MyHotelWebsite.Web.ViewModels.Reservations
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class ReservationAllViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleReservationViewModel> Reservations { get; set; } = new List<SingleReservationViewModel>();
    }
}
