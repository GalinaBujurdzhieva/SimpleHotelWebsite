using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyHotelWebsite.Web.ViewModels.Dishes;
using MyHotelWebsite.Services.Data;

namespace MyHotelWebsite.Web.Controllers
{
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
            var model = new DishesAllViewModel
            {
                ItemsPerPage = DishesPerPage,
                AllEntitiesCount = await this.dishesService.GetCountAsync(),
                Dishes = await this.dishesService.GetRandomDishesAsync<SingleDishViewModel>(),
                PageNumber = id,
            };

            return this.View(model);
        }
    }
}
