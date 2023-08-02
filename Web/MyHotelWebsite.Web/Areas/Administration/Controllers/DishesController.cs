namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Dishes;
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;
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
            var model = new AllDishViewModel
            {
                ItemsPerPage = DishesPerPage,
                AllEntitiesCount = await this.dishesService.GetCountAsync(),
                Dishes = await this.dishesService.GetAllDishesAsync<SingleDishViewModel>(id, DishesPerPage),
                PageNumber = id,
            };
            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
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
            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditDishViewModel model, IFormFile? file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this.dishesService.EditDishAsync(model, id, staffId, $"{this.environment.WebRootPath}/images", file);
                this.TempData["Message"] = "Dish changed successfully.";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Could not edit this dish");
                this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ByCategory(DishCategory dishCategory, bool isReady, bool? isInStock, DishSorting sorting, int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int DishesPerPage = 12;

            var dishesWithFilter = await this.dishesService.GetDishesByDishCategoryAsync<SingleDishViewModel>(id, dishCategory, sorting, isInStock, isReady, DishesPerPage);
            var model = new DishByCategoryViewModel
            {
                ItemsPerPage = DishesPerPage,
                AllEntitiesCount = await this.dishesService.GetCountOfDishesByCategoryAsync(isInStock, isReady, dishCategory, sorting),
                Dishes = dishesWithFilter,
                PageNumber = id,
                DishCategory = dishCategory.ToString(),
                IsReady = isReady,
                IsInStock = isInStock,
                Sorting = sorting,
            };
            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(model);
        }

        public IActionResult Search()
        {
            this.DropDownReBind();
            return this.View();
        }

        private void DropDownReBind()
        {
            List<SelectListItem> boolYesOrNo = new List<SelectListItem>();

            boolYesOrNo.Add(new SelectListItem
            {
                Text = "Yes",
                Value = true.ToString(),
            });
            boolYesOrNo.Add(new SelectListItem
            {
                Text = "No",
                Value = false.ToString(),
            });

            this.ViewData["boolYesOrNo"] = boolYesOrNo;
        }
    }
}
