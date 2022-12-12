namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;
    using MyHotelWebsite.Web.ViewModels.Dishes;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.ChefRoleName)]
    public class DishesController : AdministrationController
    {
        private readonly IDishesService dishesService;
        private readonly IWebHostEnvironment environment;

        public DishesController(IDishesService dishesService, IWebHostEnvironment environment)
        {
            this.dishesService = dishesService;
            this.environment = environment;
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

        public IActionResult Create()
        {
            var model = new CreateDishViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDishViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.dishesService.AddDishAsync(model, staffId, $"{this.environment.WebRootPath}/images");
                this.TempData["Message"] = "Dish added successfully.";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Could not add this dish");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!await this.dishesService.DoesDishExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.dishesService.DeleteDishAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (!await this.dishesService.DoesDishExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.dishesService.DishDetailsByIdAsync<EditDishViewModel>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditDishViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this.dishesService.EditDishAsync(model, id, staffId, $"{this.environment.WebRootPath}/images");
                this.TempData["Message"] = "Dish changed successfully.";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Could not edit this dish");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ByCategory(DishCategory dishCategory, int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int DishesPerPage = 12;

            var dishesWithFilter = await this.dishesService.GetDishesByDishCategoryAsync<SingleDishViewModel>(id, dishCategory, DishesPerPage);
            var model = new DishByCategoryViewModel
            {
                ItemsPerPage = DishesPerPage,
                AllEntitiesCount = await this.dishesService.GetCountOfDishesByCategoryAsync(dishCategory),
                Dishes = dishesWithFilter,
                PageNumber = id,
                DishCategory = dishCategory.ToString(),
            };
            return this.View(model);
        }

        public IActionResult Search()
        {
            return this.View();
        }
    }
}
