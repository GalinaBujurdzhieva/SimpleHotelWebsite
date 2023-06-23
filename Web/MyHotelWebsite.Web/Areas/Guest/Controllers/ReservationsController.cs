namespace MyHotelWebsite.Web.Areas.Guest.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.Controllers;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;
    using MyHotelWebsite.Web.ViewModels.Reservations;

    [Area("Guest")]
    public class ReservationsController : BaseController
     {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGuestsService guestsService;
        private readonly IRoomsService roomsService;

        public ReservationsController(UserManager<ApplicationUser> userManager, IGuestsService guestsService, IRoomsService roomsService)
        {
            this.userManager = userManager;
            this.guestsService = guestsService;
            this.roomsService = roomsService;
        }

        [Authorize(Roles = GlobalConstants.GuestRoleName)]
        public async Task<IActionResult> Book()
        {
            ApplicationUser guestId = await this.userManager.GetUserAsync(this.User);
            string guestEmail = guestId.Email;  // await this.guestsService.GetGuestEmailAsync(this.User);
            string guestPhoneNumber = guestId.PhoneNumber; // await this.guestsService.GetGuestPhoneNumberAsync(this.User);
            var model = new AddReservationViewModel()
            {
                Email = guestEmail,
                PhoneNumber = guestPhoneNumber,
                AccommodationDate = DateTime.UtcNow,
                ReleaseDate = DateTime.UtcNow.AddDays(1),
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.GuestRoleName)]
        public async Task<IActionResult> Book(AddReservationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var roomsByType = await this.roomsService.GetAllRoomsByRoomTypeAsync<SingleRoomViewModel>(model.RoomType);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
