namespace MyHotelWebsite.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum OrderStatus
    {
        New = 1,

        InProgress = 2,

        Ready = 3,

        TakenToTheGuest = 4,
    }
}
