namespace MyHotelWebsite.Web.ViewModels.Blogs
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class AllBlogViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleBlogViewModel> Blogs { get; set; } = new List<SingleBlogViewModel>();
    }
}
