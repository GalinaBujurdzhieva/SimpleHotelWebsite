namespace MyHotelWebsite.Web.ViewModels.Administration.Reservations
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;

    public class HotelAdministrationEditReservationViewModel : HotelAdministrationAddReservationViewModel
    {
        public int Id { get; set; }
    }
}
