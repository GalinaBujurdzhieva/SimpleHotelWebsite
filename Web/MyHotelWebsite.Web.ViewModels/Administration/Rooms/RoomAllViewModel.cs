namespace MyHotelWebsite.Web.ViewModels.Administration.Rooms
{
    using System.Collections.Generic;
    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class RoomAllViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleRoomViewModel> Rooms { get; set; } = new List<SingleRoomViewModel>();
    }
}
