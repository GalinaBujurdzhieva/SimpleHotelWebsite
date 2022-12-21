namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;

    public interface IRoomsService
    {
        Task CleanRoomAsync(int id, string applicationUserId);

        Task<bool> DoesRoomExistAsync(int id);

        Task EditRoomAsync(EditRoomViewModel model, int id, string applicationUserId);

        Task<IEnumerable<T>> GetAllFreeRoomsAtTheMomentAsync<T>();

        Task<IEnumerable<T>> GetAllFreeRoomsForACertainPeriodOfTimeAsync<T>(DateTime accommodationDate, DateTime releaseDate);

        Task<IEnumerable<T>> GetAllRoomsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetAllRoomsByCapacityAsync<T>(int capacity);

        Task<IEnumerable<T>> GetAllRoomsByRoomTypeAsync<T>(RoomType roomType);

        Task<int> GetCountAsync();

        Task<int> GetCountOfRoomsByFourCriteriaAsync(bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0);

        Task<T> RoomDetailsByIdAsync<T>(int id);

        Task<IEnumerable<T>> SearchRoomsByFourCriteriaAsync<T>(int page, bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0, int itemsPerPage = 4);
    }
}
