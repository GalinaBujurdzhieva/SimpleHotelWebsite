namespace MyHotelWebsite.Web.ViewModels.Administration.Rooms
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class RoomByFourCriteriaSearchViewModel : PagingRoomsByFourCriteriaSearchViewModel
    {
        public IEnumerable<SingleRoomViewModel> Rooms { get; set; } = new List<SingleRoomViewModel>();
    }
}
