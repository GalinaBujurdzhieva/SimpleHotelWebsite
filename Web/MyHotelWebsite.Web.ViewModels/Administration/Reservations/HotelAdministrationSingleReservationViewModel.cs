namespace MyHotelWebsite.Web.ViewModels.Administration.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class HotelAdministrationSingleReservationViewModel : IMapFrom<Reservation>
    {
        public int Id { get; set; }

        [Display(Name = "Email")]
        public string ReservationEmail { get; set; }

        [Display(Name = "Phone Number")]
        public string ReservationPhone { get; set; }

        [Display(Name = "Check in")]
        [DataType(DataType.Date)]
        public DateTime AccommodationDate { get; set; }

        [Display(Name = "Check out")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Adults Count")]
        public int AdultsCount { get; set; }

        [Display(Name = "Children Count")]
        public int ChildrenCount { get; set; }

        [Display(Name = "Room Type")]
        public RoomType RoomType { get; set; }

        public Catering Catering { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
