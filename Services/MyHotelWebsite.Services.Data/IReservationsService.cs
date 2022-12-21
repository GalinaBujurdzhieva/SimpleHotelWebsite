namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Reservations;

    public interface IReservationsService
    {
        Task<int> GetCountAsync();

        Task AddReservationAsync(AddReservationViewModel model);
    }
}
