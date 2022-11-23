namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Blogs;

    public interface IBlogsService
    {
        IEnumerable<T> GetAllBlogs<T>(int page, int itemsPerPage = 4);

        IEnumerable<T> GetLastBlogs<T>(int count);
    }
}
