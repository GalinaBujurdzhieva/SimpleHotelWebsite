namespace MyHotelWebsite.Web.ViewModels.Pagination
{
    using System;

    using MyHotelWebsite.Web.ViewModels.Administration.Enums;

    public class PagingReservationsByFiveCriteriaSearchViewModel : PagingAllViewModel
    {
        public string Catering { get; set; }

        public string RoomType { get; set; }

        public string ReservationEmail { get; set; }

        public string ReservationPhone { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ReservationSorting Sorting { get; set; }
    }
}
