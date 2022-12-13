namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    public interface IReservationsService
    {
        Task<int> GetCountAsync();
    }
}
