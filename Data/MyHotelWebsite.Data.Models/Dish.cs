namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyHotelWebsite.Data.Common.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class Dish : BaseDeletableModel<string>
    {
        public Dish()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DishOrders = new HashSet<DishOrder>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public DishCategory DishCategory { get; set; }

        public string DishImageUrl { get; set; }

        public string DishImageId { get; set; }

        public virtual DishImage DishImage { get; set; }

        public int QuantityInStock { get; set; }

        public bool IsReady { get; set; }

        public ICollection<DishOrder> DishOrders { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
