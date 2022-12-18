namespace MyHotelWebsite.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Dishes;

    public class DishesController : BaseController
    {
        private readonly IDishesService dishesService;

        public DishesController(IDishesService dishesService)
        {
            this.dishesService = dishesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int DishesPerPage = 12;
            var model = new DishAllViewModel
            {
                ItemsPerPage = DishesPerPage,
                AllEntitiesCount = await this.dishesService.GetCountAsync(),
                Dishes = await this.dishesService.GetAllDishesAsync<SingleDishViewModel>(id, DishesPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }

        public async Task<IActionResult> ByNameAndCategory(string name, DishCategory dishCategory, int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int DishesPerPage = 12;

            var dishesWithFilter = await this.dishesService.SearchDishesByNameAndCategoryAsync<SingleDishViewModel>(id, name, dishCategory, DishesPerPage);
            var model = new DishByNameAndCategoryViewModel
            {
                ItemsPerPage = DishesPerPage,
                AllEntitiesCount = await this.dishesService.GetCountOfDishesByNameAndCategoryAsync(name, dishCategory),
                Dishes = dishesWithFilter,
                PageNumber = id,
                DishCategory = dishCategory.ToString(),
            };
            return this.View(model);
        }
    }
}
