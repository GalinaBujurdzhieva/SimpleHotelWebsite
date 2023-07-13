namespace MyHotelWebsite.Web.ViewModels.Pagination
{
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;

    public class PagingDishesByCategoryViewModel : PagingAllViewModel
    {
        public string DishCategory { get; set; }

        public bool IsReady { get; set; }

        public bool? IsInStock { get; set; }

        public DishSorting Sorting { get; set; }
    }
}
