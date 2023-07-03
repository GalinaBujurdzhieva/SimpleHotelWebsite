namespace MyHotelWebsite.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class AllDishViewModel : PagingAllViewModel
    {
        public IEnumerable<SingleDishViewModel> Dishes { get; set; } = new List<SingleDishViewModel>();
    }
}
