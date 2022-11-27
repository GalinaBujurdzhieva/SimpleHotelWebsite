namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyHotelWebsite.Web.ViewModels.Administration.Blogs;
    using MyHotelWebsite.Web.ViewModels.Blogs;

    public interface IBlogsService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetAllBlogsAsync<T>(int page, int itemsPerPage = 4);

        Task<IEnumerable<T>> GetLastBlogsAsync<T>(int count);

        Task<T> BlogDetailsByIdAsync<T>(int id);

        Task<bool> DoesBlogExistsAsync(int id);

        Task AddBlogAsync(CreateBlogViewModel model, string staffId, string imagePath);
    }
}
