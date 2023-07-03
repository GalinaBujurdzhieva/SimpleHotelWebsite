namespace MyHotelWebsite.Web.ViewModels.Administration.Reservations
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;

    public class HotelAdministrationAddReservationViewModel : AddReservationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public override string ReservationEmail { get => base.ReservationEmail; set => base.ReservationEmail = value; }

        [Required]
        [StringLength(15)]
        [Phone]
        [Display(Name = "Phone Number")]

        public override string ReservationPhone { get => base.ReservationPhone; set => base.ReservationPhone = value; }
    }
}
