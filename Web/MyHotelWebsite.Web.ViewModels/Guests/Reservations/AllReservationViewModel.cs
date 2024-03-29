﻿namespace MyHotelWebsite.Web.ViewModels.Guests.Reservations
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class AllReservationViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleReservationViewModel> Reservations { get; set; } = new List<SingleReservationViewModel>();
    }
}
