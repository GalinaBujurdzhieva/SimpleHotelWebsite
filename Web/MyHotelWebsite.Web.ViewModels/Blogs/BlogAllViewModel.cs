namespace MyHotelWebsite.Web.ViewModels.Blogs
{
    using System.Collections.Generic;

    public class BlogAllViewModel : PagingViewModel
    {
        public IEnumerable<SingleBlogViewModel> Blogs { get; set; } = new List<SingleBlogViewModel>();
    }
}
