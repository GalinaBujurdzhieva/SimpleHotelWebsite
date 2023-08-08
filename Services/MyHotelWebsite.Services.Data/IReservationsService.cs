namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Reservations;
    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;
    using Syncfusion.Pdf;

    public interface IReservationsService
    {
        Task AddReservationAsync(AddReservationViewModel model, string applicationUserId);

        Task DeleteReservationAsync(int id);

        Task<bool> DoesReservationExistsAsync(int id);

        Task EditReservationAsync(EditReservationViewModel model, int id, string applicationUserId);

        Task<PdfDocument> FillPdfReservationAsync(int id);

        Task<List<object>> FillPdfTableWithDetails(int id);

        Task<int> GetCountAsync();

        Task<int> GetCountOfAllCurrentReservationsAsync();

        Task<int> GetCountOfAllPastReservationsAsync();

        Task<int> GetCountOfAllUpcomingReservationsAsync();

        Task<int> GetCountOfMyReservationsAsync(string applicationUserId);

        Task<string> GetGuestEmail(int reservationId);

        Task<string> GetGuestPhoneNumber(int reservationId);

        Task<IEnumerable<T>> GetMyReservationsAsync<T>(string applicationUserId, int page, int itemsPerPage = 4);

        Task<decimal> GetReservationTotalPrice(RoomType roomType, DateTime accomodationDate, DateTime releaseDate, int adultsCount, int childrenCount);

        Task HotelAdministrationCreateReservationAsync(HotelAdministrationAddReservationViewModel model);

        Task HotelAdministrationEditReservationAsync(HotelAdministrationEditReservationViewModel model, int id);

        Task<IEnumerable<T>> HotelAdministrationGetAllReservationsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> HotelAdministrationGetAllCurrentReservationsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> HotelAdministrationGetAllPastReservationsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> HotelAdministrationGetAllUpcomingReservationsAsync<T>(int page, int itemsPerPage = 4);

        Task<int> HotelAdministrationGetCountOfReservationsByFiveCriteriaAsync(string reservationEmail = null, string reservationPhone = null, Catering catering = 0, RoomType roomType = 0, ReservationSorting sorting = ReservationSorting.AccommodationDate);

        Task<IEnumerable<T>> HotelAdministrationGetReservationsByFiveCriteriaAsync<T>(int page, Catering catering, RoomType roomType, ReservationSorting sorting, string reservationEmail = null, string reservationPhone = null, int itemsPerPage = 4);

        Task<T> HotelAdministrationReservationDetailsByIdAsync<T>(int id);

        Task HotelAdministrationReserveRoomAsync(HotelAdministrationReserveRoomViewModel model, int id);

        Task<bool> IsReservationActiveAtTheMoment(int id);

        Task<T> ReservationDetailsByIdAsync<T>(int id);
    }
}
