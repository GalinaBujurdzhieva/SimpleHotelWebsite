namespace MyHotelWebsite.Web.Areas.Guest.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.Controllers;
    using MyHotelWebsite.Web.ViewModels.Dishes;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    [Area("Guest")]
    [Authorize(Roles = GlobalConstants.GuestRoleName)]
    public class ShoppingCartsController : BaseController
    {
        private readonly IDishesService dishesService;
        private readonly IShoppingCartsService shoppingCartsService;

        public ShoppingCartsController(IDishesService dishesService, IShoppingCartsService shoppingCartsService)
        {
            this.dishesService = dishesService;
            this.shoppingCartsService = shoppingCartsService;
        }

        public async Task<IActionResult> Buy(string id)
        {
            if (!await this.dishesService.DoesDishExistsAsync(id))
            {
                return this.RedirectToAction("All", "Dishes", new { area = string.Empty });
            }

            var model = new SingleShoppingCartViewModel
            {
                DishId = id,
                Dish = await this.dishesService.DishDetailsByIdAsync<SingleDishViewModel>(id),
                Count = 1,
                ApplicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(SingleShoppingCartViewModel shoppingCart)
        {
            bool dishAlreadyIsInTheShoppingCart = await this.shoppingCartsService.IsDishAlreadyInTheShoppingCartOfThatUserAsync(shoppingCart.DishId, shoppingCart.ApplicationUserId);
            if (!dishAlreadyIsInTheShoppingCart)
            {
                await this.shoppingCartsService.AddDishInTheShoppingCartAsync(shoppingCart);
            }
            else
            {
                await this.shoppingCartsService.UpdateDishCountInTheShoppingCartAsync(shoppingCart.DishId, shoppingCart.ApplicationUserId, shoppingCart.Count);
            }

            return this.RedirectToAction("All", "Dishes", new { area = string.Empty });
        }

        public async Task<IActionResult> Index()
        {
            var model = new AllShoppingCartsOfOneUserViewModel();
            string applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(applicationUserId))
            {
                model.ShoppingCartsList = await this.shoppingCartsService.GetAllSingleShoppingCartsOfTheUser<SingleShoppingCartViewModel>(applicationUserId);
            }

            return this.View(model);
        }
    }
}
