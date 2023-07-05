namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Reservations;
    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;

    public interface IReservationsService
    {
        Task AddReservationAsync(AddReservationViewModel model, string applicationUserId);

        Task DeleteReservationAsync(int id);

        Task<bool> DoesReservationExistsAsync(int id);

        Task EditReservationAsync(EditReservationViewModel model, int id, string applicationUserId);

        Task<List<object>> FillPdf(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountOfMyReservationsAsync(string applicationUserId);

        Task<string> GetGuestEmail(int reservationId);

        Task<string> GetGuestPhoneNumber(int reservationId);

        Task<IEnumerable<T>> GetMyReservationsAsync<T>(string applicationUserId, int page, int itemsPerPage = 4);

        Task<decimal> GetReservationTotalPrice(RoomType roomType, DateTime accomodationDate, DateTime releaseDate, int adultsCount, int childrenCount);

        Task HotelAdministrationCreateReservationAsync(HotelAdministrationAddReservationViewModel model);

        Task HotelAdministrationEditReservationAsync(HotelAdministrationEditReservationViewModel model, int id);

        Task<IEnumerable<T>> HotelAdministrationGetAllReservationsAsync<T>(int page, int itemsPerPage = 4);

        Task<T> HotelAdministrationReservationDetailsByIdAsync<T>(int id);

        Task<bool> IsReservationActiveAtTheMoment(int id);

        Task<T> ReservationDetailsByIdAsync<T>(int id);
    }
}
