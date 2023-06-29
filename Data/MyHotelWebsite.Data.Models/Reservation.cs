namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Common.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class Reservation : BaseDeletableModel<int>
    {
        public Reservation()
        {
            this.RoomReservations = new HashSet<RoomReservation>();
        }

        [Display(Name ="Check in")]
        public DateTime AccommodationDate { get; set; }

        [Display(Name = "Check out")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Adults Count")]
        public int AdultsCount { get; set; }

        [Display(Name = "Children Count")]
        public int? ChildrenCount { get; set; }

        [Display(Name = "Room Type")]
        public RoomType RoomType { get; set; }

        public Catering Catering { get; set; }

        public decimal TotalPrice { get; set; }

        public string ReservationEmail { get; set; }

        public string ReservationPhone { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<RoomReservation> RoomReservations { get; set; }
    }
}
