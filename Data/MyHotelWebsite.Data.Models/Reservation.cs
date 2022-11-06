namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Common.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class Reservation : BaseDeletableModel<int>
    {
        public Reservation()
        {
            this.RoomReservations = new HashSet<RoomReservation>();
        }

        public DateTime AccommodationDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Catering Catering { get; set; }

        public string GuestId { get; set; }

        public virtual Guest Guest { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual ICollection<RoomReservation> RoomReservations { get; set; }
    }
}
