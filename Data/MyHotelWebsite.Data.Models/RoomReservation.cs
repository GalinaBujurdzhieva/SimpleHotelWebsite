namespace MyHotelWebsite.Data.Models
{
    using MyHotelWebsite.Data.Common.Models;

    public class RoomReservation : BaseDeletableModel<int>
    {
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
