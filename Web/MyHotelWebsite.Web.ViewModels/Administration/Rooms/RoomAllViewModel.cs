namespace MyHotelWebsite.Web.ViewModels.Administration.Rooms
{
    using System.Collections.Generic;

    public class RoomAllViewModel : PagingViewModel
    {
        public IEnumerable<SingleRoomViewModel> Rooms { get; set; } = new List<SingleRoomViewModel>();
    }
}
