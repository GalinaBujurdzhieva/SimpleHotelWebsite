namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoomReservation
    {
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
