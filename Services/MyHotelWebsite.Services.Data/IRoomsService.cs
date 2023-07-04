namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;
    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;

    public interface IRoomsService
    {
        Task CleanRoomAsync(int id, string applicationUserId);

        Task<bool> DoesRoomExistAsync(int id);

        Task EditRoomAsync(EditRoomViewModel model, int id, string applicationUserId);

        Task<int> GetAdultsCountAsync(int id);

        Task<IEnumerable<T>> GetAllFreeRoomsAtTheMomentAsync<T>();

        Task<IEnumerable<T>> GetAllFreeRoomsForACertainPeriodOfTimeAsync<T>(DateTime accommodationDate, DateTime releaseDate);

        Task<IEnumerable<T>> GetAllRoomsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetAllRoomsByCapacityAsync<T>(int capacity);

        Task<IEnumerable<T>> GetAllFreeRoomsByRoomTypeAsync<T>(RoomType roomType);

        Task<int> GetCountAsync();

        Task<int> GetCountOfRoomsByFourCriteriaAsync(bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0);

        Task<RoomType> GetRoomTypeByIdAsync(int id);

        Task LeaveOccupiedRoomsAsync();

        Task OccupyRoomsAsync();

        Task RemoveIsReservedPropertyOfNotReservedRooms();

        Task<int> ReserveRoomAsync(RoomType roomType, DateTime accommodationDate, DateTime releaseDate);

        Task<bool> ReserveRoomByIdAsync(int roomId, DateTime accommodationDate, DateTime releaseDate);

        Task<T> RoomDetailsByIdAsync<T>(int id);

        Task<IEnumerable<T>> SearchRoomsByFourCriteriaAsync<T>(int page, bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0, int itemsPerPage = 4);
    }
}
