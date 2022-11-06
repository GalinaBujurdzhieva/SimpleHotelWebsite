namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Common.Models;

    public class DishOrder : BaseModel<int>
    {
        public int DishId { get; set; }

        public virtual Dish Dish { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int DishQuantity { get; set; }

        public decimal TotalPrice
            => this.DishQuantity * this.Dish.Price;
    }
}
