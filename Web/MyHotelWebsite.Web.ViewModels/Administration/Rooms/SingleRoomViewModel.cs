namespace MyHotelWebsite.Web.ViewModels.Administration.Rooms
{
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class SingleRoomViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int Capacity { get; set; }

        public RoomType RoomType { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildrenPrice { get; set; }

        public bool IsReserved { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsCleaned { get; set; }
    }
}
