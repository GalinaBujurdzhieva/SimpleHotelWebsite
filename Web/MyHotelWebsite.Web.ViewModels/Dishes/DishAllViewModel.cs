namespace MyHotelWebsite.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    public class DishAllViewModel : PagingViewModel
    {
        public IEnumerable<SingleDishViewModel> Dishes { get; set; } = new List<SingleDishViewModel>();
    }
}
