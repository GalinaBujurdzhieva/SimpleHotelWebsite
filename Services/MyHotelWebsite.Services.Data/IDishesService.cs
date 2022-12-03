namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDishesService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetRandomDishesAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetHotDrinksAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetColdDrinksAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetAlcoholDrinksAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetAppetizersAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetGourmetsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetSaladsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetMainCoursesAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetDessertsAsync<T>(int page, int itemsPerPage = 4);
    }
}
