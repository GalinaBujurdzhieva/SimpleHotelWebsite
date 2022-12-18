namespace MyHotelWebsite.Web.ViewModels.Pagination
{
    using MyHotelWebsite.Data.Models.Enums;

    public class PagingRoomsByFourCriteriaSearchViewModel : PagingAllViewModel
    {
        public string RoomType { get; set; }

        public bool IsReserved { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsCleaned { get; set; }
    }
}
