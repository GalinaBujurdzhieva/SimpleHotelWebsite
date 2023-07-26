namespace MyHotelWebsite.Web.ViewComponents
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public class ShoppingCartItemsCountViewComponent : ViewComponent
    {
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingCartItemsCountViewComponent(IShoppingCartsService shoppingCartsService, IHttpContextAccessor httpContextAccessor)
        {
               this.shoppingCartsService = shoppingCartsService;
               this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AllShoppingCartsOfOneUserViewModel();
            var applicationUserId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(applicationUserId))
            {
                model.ShoppingCartsList = await this.shoppingCartsService.GetAllSingleShoppingCartsOfTheUser(applicationUserId);
            }

            return this.View(model);
        }
    }
}
