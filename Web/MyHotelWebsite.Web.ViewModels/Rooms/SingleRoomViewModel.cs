namespace MyHotelWebsite.Web.ViewModels.Rooms
{
    using AutoMapper;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class SingleRoomViewModel : IMapFrom<Room>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; }

        public RoomType RoomType { get; set; }

        public decimal AdultPrice { get; set; }

        public decimal ChildrenPrice { get; set; }

        public bool IsReserved { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsCleaned { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Room, SingleRoomViewModel>()
                .ForMember(x => x.Floor, opt => opt.MapFrom(r => r.Floor));
        }
    }
}
