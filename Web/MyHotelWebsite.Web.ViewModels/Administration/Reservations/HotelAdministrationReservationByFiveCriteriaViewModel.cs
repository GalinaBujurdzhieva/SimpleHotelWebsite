namespace MyHotelWebsite.Web.ViewModels.Administration.Reservations
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class HotelAdministrationReservationByFiveCriteriaViewModel : PagingReservationsByFiveCriteriaSearchViewModel
    {
        public IEnumerable<HotelAdministrationSingleReservationViewModel> Reservations { get; set; } = new List<HotelAdministrationSingleReservationViewModel>();
    }
}
