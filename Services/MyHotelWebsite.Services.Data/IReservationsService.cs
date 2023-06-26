namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;

    public interface IReservationsService
    {
        Task AddReservationAsync(AddReservationViewModel model, string applicationUserId);

        Task DeleteReservationAsync(int id);

        Task<bool> DoesReservationExistsAsync(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountOfMyReservationsAsync(string applicationUserId);

        Task<IEnumerable<T>> GetMyReservationsAsync<T>(string applicationUserId, int page, int itemsPerPage = 4);

        Task<decimal> GetReservationTotalPrice(RoomType roomType, DateTime accomodationDate, DateTime releaseDate, int adultsCount, int childrenCount);

        Task EditReservationAsync(EditReservationViewModel model, int id, string applicationUserId);

        Task<T> ReservationDetailsByIdAsync<T>(int id);
    }
}
