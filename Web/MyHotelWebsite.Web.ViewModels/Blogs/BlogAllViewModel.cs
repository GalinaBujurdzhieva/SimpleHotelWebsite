using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Web.ViewModels.Blogs
{
    public class BlogAllViewModel : PagingViewModel
    {
        public IEnumerable<SingleBlogViewModel> Blogs { get; set; } = new List<SingleBlogViewModel>();
    }
}
