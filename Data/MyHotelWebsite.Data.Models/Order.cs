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

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.DishOrders = new HashSet<DishOrder>();
        }

        [StringLength(200)]
        public string Comment { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<DishOrder> DishOrders { get; set; }
    }
}
