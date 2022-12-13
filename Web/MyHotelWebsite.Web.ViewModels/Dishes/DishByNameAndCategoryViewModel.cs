namespace MyHotelWebsite.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    using MyHotelWebsite.Web.ViewModels.Pagination;

    public class DishByNameAndCategoryViewModel : PagingDishesByNameAndCategoryViewModel
    {
        public IEnumerable<SingleDishViewModel> Dishes { get; set; } = new List<SingleDishViewModel>();
    }
}
