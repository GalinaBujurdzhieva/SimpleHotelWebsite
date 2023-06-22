namespace MyHotelWebsite.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class SingleReservationViewModel : IMapFrom<Reservation>
    {
        public DateTime AccommodationDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AdultsCount { get; set; }

        public int ChildrenCount { get; set; }

        public RoomType RoomType { get; set; }

        public Catering Catering { get; set; }

        [Display(Name = "Guest Name")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
