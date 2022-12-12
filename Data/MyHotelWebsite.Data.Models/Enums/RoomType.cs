namespace MyHotelWebsite.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public enum RoomType
    {
        [Display(Name = "Single Room")]
        SingleRoom = 1,

        [Display(Name = "Double Room")]
        DoubleRoom = 2,

        Studio = 3,

        Apartment = 4,
    }
}
