namespace MyHotelWebsite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GuestsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
