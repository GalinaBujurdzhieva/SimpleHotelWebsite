namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<int> GetCountAsync();
    }
}
