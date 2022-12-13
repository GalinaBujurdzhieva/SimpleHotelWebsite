namespace MyHotelWebsite.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    internal class RoomAllViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleRoomViewModel> Rooms { get; set; } = new List<SingleRoomViewModel>();
    }
}
