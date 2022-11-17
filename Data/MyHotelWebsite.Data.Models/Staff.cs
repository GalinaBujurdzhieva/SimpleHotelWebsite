namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Common.Models;

    public class Staff : BaseDeletableModel<string>
    {
        public Staff()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Orders = new HashSet<Order>();

            this.Reservations = new HashSet<Reservation>();

            this.Rooms = new HashSet<Room>();

            this.Blogs = new HashSet<Blog>();

            this.Dishes = new HashSet<Dish>();
        }

        [Required]
        [StringLength(20)]
        public string JobTitle { get; set; }

        public string JobTitleImageUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
