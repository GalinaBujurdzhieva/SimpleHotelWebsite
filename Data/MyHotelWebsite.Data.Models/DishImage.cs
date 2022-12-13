namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Common.Models;

    public class DishImage : BaseDeletableModel<string>
    {
        public DishImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public string Extension { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
