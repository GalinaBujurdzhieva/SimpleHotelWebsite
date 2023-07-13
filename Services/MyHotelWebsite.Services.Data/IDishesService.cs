namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;

    public interface IDishesService
    {
        Task AddDishAsync(CreateDishViewModel model, string applicationUserId, string imagePath);

        Task DeleteDishAsync(string id);

        Task<T> DishDetailsByIdAsync<T>(string id);

        Task<bool> DoesDishExistsAsync(string id);

        Task EditDishAsync(EditDishViewModel model, string id, string applicationUserId, string imagePath, IFormFile? file);

        Task<IEnumerable<T>> GetAllDishesAsync<T>(int page, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task<int> GetCountOfDishesByCategoryAsync(bool? isInStock = null, bool isReady = false, DishCategory dishCategory = 0, DishSorting sorting = DishSorting.Name);

        Task<int> GetCountOfDishesByNameAndCategoryAsync(string name = null, DishCategory dishCategory = 0);

        Task<IEnumerable<T>> GetDishesByDishCategoryAsync<T>(int page, DishCategory dishCategory, DishSorting sorting, bool? isInStock = null, bool isReady = false, int itemsPerPage = 4);

        Task<IEnumerable<T>> SearchDishesByNameAndCategoryAsync<T>(int page, string name = null, DishCategory dishCategory = 0, int itemsPerPage = 4);
    }
}
