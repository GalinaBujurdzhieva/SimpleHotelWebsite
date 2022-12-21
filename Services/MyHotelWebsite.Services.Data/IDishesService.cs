namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;

    public interface IDishesService
    {
        Task AddDishAsync(CreateDishViewModel model, string applicationUserId, string imagePath);

        Task DeleteDishAsync(string id);

        Task<T> DishDetailsByIdAsync<T>(string id);

        Task<bool> DoesDishExistsAsync(string id);

        Task EditDishAsync(EditDishViewModel model, string id, string applicationUserId, string imagePath);

        Task<IEnumerable<T>> GetAllDishesAsync<T>(int page, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task<int> GetCountOfDishesByCategoryAsync(DishCategory dishCategory);

        Task<int> GetCountOfDishesByNameAndCategoryAsync(string name = null, DishCategory dishCategory = 0);

        Task<IEnumerable<T>> GetDishesByDishCategoryAsync<T>(int page, DishCategory dishCategory, int itemsPerPage = 4);

        Task<IEnumerable<T>> SearchDishesByNameAndCategoryAsync<T>(int page, string name = null, DishCategory dishCategory = 0, int itemsPerPage = 4);
    }
}
