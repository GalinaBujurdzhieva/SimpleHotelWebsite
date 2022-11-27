using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Web.ViewModels.ViewComponents
{
    public class DashboardStatisticsViewModel
    {
        public int BlogsCount { get; set; }

        public int DishesCount { get; set; }

        public int OrdersCount { get; set; }

        public int RoomsCount { get; set; }

        public int GuestsCount { get; set; }

        public int ReservationsCount { get; set; }
    }
}
