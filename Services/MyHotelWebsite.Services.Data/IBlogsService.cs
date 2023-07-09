namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;

    public interface IBlogsService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetAllBlogsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetLastBlogsAsync<T>(int count);

        Task<T> BlogDetailsByIdAsync<T>(int id);

        Task<BlogImage> BlogImageByBlogIdAsync(int id);

        Task<bool> DoesBlogExistsAsync(int id);

        Task AddBlogAsync(CreateBlogViewModel model, string applicationUserId, string imagePath);

        Task EditBlogAsync(EditBlogViewModel model, int id, string applicationUserId, string imagePath, IFormFile file);

        Task DeleteBlogAsync(int id);
    }
}
