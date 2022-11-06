﻿namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Common.Models;
    using MyHotelWebsite.Data.Models.Enums;

    public class Dish : BaseDeletableModel<int>
    {
        public Dish()
        {
            this.DishOrders = new HashSet<DishOrder>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public DishCategory DishCategory { get; set; }

        public string DishImageUrl { get; set; }

        public int QuantityInStock { get; set; }

        public ICollection<DishOrder> DishOrders { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
