namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;

    public interface IRoomsService
    {
        Task CleanRoomAsync(int id, string staffId);

        Task<bool> DoesRoomExistAsync(int id);

        Task EditRoomAsync(EditRoomViewModel model, int id, string staffId);

        Task<IEnumerable<T>> GetAllRoomsAsync<T>(int page, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task<int> GetCountOfRoomsByFourCriteriaAsync(bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0);

        Task<T> RoomDetailsByIdAsync<T>(int id);

        Task<IEnumerable<T>> SearchRoomsByFourCriteriaAsync<T>(int page, bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0, int itemsPerPage = 4);
    }
}
