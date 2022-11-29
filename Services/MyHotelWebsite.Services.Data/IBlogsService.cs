namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;

    public interface IBlogsService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetAllBlogsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetLastBlogsAsync<T>(int count);

        Task<T> BlogDetailsByIdAsync<T>(int id);

        Task<bool> DoesBlogExistsAsync(int id);

        Task AddBlogAsync(CreateBlogViewModel model, string staffId, string imagePath);

        Task EditBlogAsync(EditBlogViewModel model, int id, string staffId, string imagePath);

        Task DeleteBlogAsync(int id);
    }
}
