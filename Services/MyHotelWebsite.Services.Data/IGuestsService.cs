namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    public interface IGuestsService
    {
        Task<int> GetCountAsync();
    }
}
