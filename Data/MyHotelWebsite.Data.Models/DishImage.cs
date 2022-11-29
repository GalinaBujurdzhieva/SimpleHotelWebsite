﻿namespace MyHotelWebsite.Data.Models
{
    using MyHotelWebsite.Data.Common.Models;
    using System;

    public class DishImage : BaseDeletableModel<string>
    {
        public DishImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public string Extension { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
