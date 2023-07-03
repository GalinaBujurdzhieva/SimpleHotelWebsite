namespace MyHotelWebsite.Web.ViewModels.Administration.Reservations
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class HotelAdministrationAllReservationViewModel : PagingAllViewModel
    {
        public IEnumerable<HotelAdministrationSingleReservationViewModel> Reservations { get; set; } = new List<HotelAdministrationSingleReservationViewModel>();
    }
}
