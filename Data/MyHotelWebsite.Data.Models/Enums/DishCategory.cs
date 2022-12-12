namespace MyHotelWebsite.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum DishCategory
    {
        [Display(Name = "Hot Drinks")]
        HotDrinks = 1,

        [Display(Name = "Cold Drinks")]
        ColdDrinks = 2,

        [Display(Name = "Alcohol Drinks")]
        AlcoholDrinks = 3,

        Appetizers = 4,

        Gourmet = 5,

        Salads = 6,

        [Display(Name = "Main Courses")]
        MainCourses = 7,

        Desserts = 8,
    }
}
