namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Reservations;

    public interface IReservationsService
    {
        Task<int> GetCountAsync();

        Task AddReservationAsync(AddReservationViewModel model, string applicationUserId);

        Task<IEnumerable<T>> GetMyReservationsAsync<T>(string applicationUserId, int page, int itemsPerPage = 4);

        Task<int> GetCountOfMyReservationsAsync(string applicationUserId);

        Task<decimal> GetReservationTotalPrice(AddReservationViewModel model);
    }
}
