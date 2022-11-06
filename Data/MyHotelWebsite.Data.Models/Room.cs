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

    public class Room : BaseDeletableModel<int>
    {
        public Room()
        {
            this.RoomReservations = new HashSet<RoomReservation>();
        }

        public int RoomNumber { get; set; }

        public int Capacity { get; set; }

        public RoomType RoomType { get; set; }

        public string RoomImageUrl { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AdultPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ChildrenPrice { get; set; }

        public bool IsReserved { get; set; }

        public bool IsOccupied { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual ICollection<RoomReservation> RoomReservations { get; set; }
    }
}
