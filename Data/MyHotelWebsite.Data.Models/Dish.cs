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

        public ICollection<DishOrder> DishOrders { get; set; }

        //[Required]
        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public bool IsReady { get; set; }
    }
}
