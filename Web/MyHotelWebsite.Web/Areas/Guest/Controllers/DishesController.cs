namespace MyHotelWebsite.Web.Areas.Guest.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.Controllers;
    using MyHotelWebsite.Web.ViewModels.Blogs;
    using MyHotelWebsite.Web.ViewModels.Dishes;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Area("Guest")]
    [Authorize(Roles = GlobalConstants.GuestRoleName)]

    public class DishesController : BaseController
    {
        private readonly IDishesService dishesService;

        public DishesController(IDishesService dishesService)
        {
            this.dishesService = dishesService;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (!await this.dishesService.DoesDishExistsAsync(id))
            {
                return this.RedirectToAction("All", "Dishes", new { area = string.Empty });
            }

            var model = new ShoppingCartViewModel
            {
                DishId = id,
                Dish = await this.dishesService.DishDetailsByIdAsync<SingleDishViewModel>(id),
                Count = 1,
                ApplicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
            };
            return this.View(model);
        }
    }
}
