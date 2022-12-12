namespace MyHotelWebsite.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public enum Catering
    {
        Breakfast = 1,

        Dinner = 2,

        [Display(Name = "Breakfast and Dinner")]
        BreakfastAndDinner = 3,

        [Display(Name = "Full Board")]
        FullBoard = 4,

        [Display(Name = "All Inclusive")]
        AllInclusive = 5,
    }
}
