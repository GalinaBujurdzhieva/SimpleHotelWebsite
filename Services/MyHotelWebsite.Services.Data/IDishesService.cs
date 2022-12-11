namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;

    public interface IDishesService
    {

        Task<int> GetCountAsync();

        Task<int> GetCountOfDishesByCategoryAsync(DishCategory dishCategory);

        Task<IEnumerable<T>> GetAllDishesAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetDishesByDishCategoryAsync<T>(int page, DishCategory dishCategory, int itemsPerPage = 4);

        Task<T> DishDetailsByIdAsync<T>(string id);

        Task<bool> DoesDishExistsAsync(string id);

        Task AddDishAsync(CreateDishViewModel model, string staffId, string imagePath);

        Task EditDishAsync(EditDishViewModel model, string id, string staffId, string imagePath);

        Task DeleteDishAsync(string id);
    }
}
