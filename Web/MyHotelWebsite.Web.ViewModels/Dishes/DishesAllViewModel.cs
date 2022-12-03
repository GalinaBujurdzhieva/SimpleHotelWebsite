namespace MyHotelWebsite.Web.ViewModels.Dishes
{
    using System.Collections.Generic;

    public class DishesAllViewModel : PagingViewModel
    {
        public IEnumerable<SingleDishViewModel> Dishes { get; set; } = new List<SingleDishViewModel>();
    }
}
