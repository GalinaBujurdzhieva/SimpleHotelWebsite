namespace MyHotelWebsite.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public enum OrderStatus
    {
        New = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        Ready = 3,

        [Display(Name = "Taken To The Guest")]
        TakenToTheGuest = 4,
    }
}
