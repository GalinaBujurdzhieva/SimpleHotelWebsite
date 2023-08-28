namespace MyHotelWebsite.Web.Areas.Guest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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
        private readonly IOrdersService ordersService;

        public ShoppingCartsController(IDishesService dishesService, IShoppingCartsService shoppingCartsService, IOrdersService ordersService)
        {
            this.dishesService = dishesService;
            this.shoppingCartsService = shoppingCartsService;
            this.ordersService = ordersService;
        }

        [BindProperty]
        public AllShoppingCartsOfOneUserViewModel AllShoppingCartModel { get; set; }

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
            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(SingleShoppingCartViewModel shoppingCart)
        {
            int currentDishQuantity = await this.dishesService.DishQuantityInStockAsync(shoppingCart.DishId);
            if (currentDishQuantity < 1)
            {
                this.TempData["Error"] = "This dish is not in stock. Please choose another one.";
                return this.RedirectToAction("All", "Dishes", new { area = string.Empty });
            }
            else
            {
                bool dishAlreadyIsInTheShoppingCart = await this.shoppingCartsService.IsDishAlreadyInTheShoppingCartOfThatUserAsync(shoppingCart.DishId, shoppingCart.ApplicationUserId);
                List<SingleShoppingCartViewModel> itemsInTheShoppingCart;

                if (!dishAlreadyIsInTheShoppingCart)
                {
                    await this.shoppingCartsService.AddDishInTheShoppingCartAsync(shoppingCart);
                    this.TempData["Message"] = "Dish added successfully to shopping cart";
                }
                else
                {
                    await this.shoppingCartsService.UpdateDishCountInTheShoppingCartAsync(shoppingCart.DishId, shoppingCart.ApplicationUserId, shoppingCart.Count);
                }

                itemsInTheShoppingCart = await this.shoppingCartsService.GetAllSingleShoppingCartsOfTheUserAsync(shoppingCart.ApplicationUserId);
                this.HttpContext.Session.SetInt32(GlobalConstants.SessionCart, itemsInTheShoppingCart.Count);
            }

            return this.RedirectToAction("All", "Dishes", new { area = string.Empty });
        }

        public async Task<IActionResult> Index()
        {
            this.AllShoppingCartModel = new();
            string applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(applicationUserId))
            {
                this.AllShoppingCartModel.ShoppingCartsList = await this.shoppingCartsService.GetAllSingleShoppingCartsOfTheUserAsync(applicationUserId);
                this.AllShoppingCartModel.TotalPrice = this.shoppingCartsService.GetOrderTotalOfShoppingCartsOfTheUser(this.AllShoppingCartModel.ShoppingCartsList);
                this.HttpContext.Session.SetInt32(GlobalConstants.SessionCart, this.AllShoppingCartModel.ShoppingCartsList.Count);
            }

            this.TempData["Domain"] = this.Request.Scheme + "://" + this.Request.Host.Value + "/";
            return this.View(this.AllShoppingCartModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            var applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(applicationUserId))
            {
                this.AllShoppingCartModel.ShoppingCartsList = await this.shoppingCartsService.GetAllSingleShoppingCartsOfTheUserAsync(applicationUserId);
                this.AllShoppingCartModel.TotalPrice = this.shoppingCartsService.GetOrderTotalOfShoppingCartsOfTheUser(this.AllShoppingCartModel.ShoppingCartsList);
            }

            try
            {
                await this.ordersService.AddOrderAsync(this.AllShoppingCartModel, applicationUserId);
                this.HttpContext.Session.Clear();
                this.TempData["Message"] = "Order placed successfully";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "You can not place an order without dishes");
                return this.View(this.AllShoppingCartModel);
            }

            return this.RedirectToAction("MyOrders", "Orders");
        }

        public async Task<IActionResult> Minus(int shoppingCartId)
        {
            await this.shoppingCartsService.DecreaseQuantityOfTheDishInTheShoppingCart(shoppingCartId);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Plus(int shoppingCartId)
        {
            await this.shoppingCartsService.IncreaseQuantityOfTheDishInTheShoppingCart(shoppingCartId);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Remove(int shoppingCartId)
        {
            await this.shoppingCartsService.RemoveDishFromTheShoppingCart(shoppingCartId);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
