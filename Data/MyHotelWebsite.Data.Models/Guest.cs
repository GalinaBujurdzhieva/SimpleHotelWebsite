namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Common.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class Guest : BaseDeletableModel<string>
    {
        public Guest()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Orders = new HashSet<Order>();

            this.Reservations = new HashSet<Reservation>();
        }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
