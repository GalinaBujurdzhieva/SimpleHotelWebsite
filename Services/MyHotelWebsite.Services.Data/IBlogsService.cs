namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;

    public interface IBlogsService
    {
        Task AddBlogAsync(CreateBlogViewModel model, string applicationUserId, string imagePath);

        Task<T> BlogDetailsByIdAsync<T>(int id);

        Task DeleteBlogAsync(int id);

        Task<bool> DoesBlogExistsAsync(int id);

        Task EditBlogAsync(EditBlogViewModel model, int id, string applicationUserId, string imagePath, IFormFile file);

        Task<IEnumerable<T>> GetAllBlogsAsync<T>(int page, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetLastBlogsAsync<T>(int count);
    }
}
