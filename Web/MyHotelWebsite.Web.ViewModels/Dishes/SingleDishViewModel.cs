namespace MyHotelWebsite.Web.ViewModels.Dishes
{
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class SingleDishViewModel : IMapFrom<Dish>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DishCategory DishCategory { get; set; }

        public string DishImageUrl { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public bool IsReady { get; set; }
    }
}
