namespace MyHotelWebsite.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Web.ViewModels.Reservations;

    [Authorize]
    public class ReservationsController : BaseController
    {
        public IActionResult Book()
        {
            var model = new AddReservationViewModel();
            return this.View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Book(AddReservationViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }
        //}
    }
}
