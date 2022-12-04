namespace MyHotelWebsite.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
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
                Dishes = await this.dishesService.GetRandomDishesAsync<SingleDishViewModel>(id, DishesPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
