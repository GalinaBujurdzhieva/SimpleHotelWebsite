﻿namespace MyHotelWebsite.Web.Areas.Guest.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.Controllers;
	using MyHotelWebsite.Web.ViewModels.Guests.Reservations;
	[Area("Guest")]
    [Authorize(Roles = GlobalConstants.GuestRoleName)]
    public class ReservationsController : BaseController
     {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGuestsService guestsService;
        private readonly IRoomsService roomsService;
        private readonly IReservationsService reservationsService;

        public ReservationsController(UserManager<ApplicationUser> userManager, IGuestsService guestsService, IRoomsService roomsService, IReservationsService reservationsService)
        {
            this.userManager = userManager;
            this.guestsService = guestsService;
            this.roomsService = roomsService;
            this.reservationsService = reservationsService;
        }

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
        public async Task<IActionResult> Book(AddReservationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO: Check if there are free rooms of this type left for this period of time. Write services. For Edit action also

            var applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.reservationsService.AddReservationAsync(model, applicationUserId);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "There are no free rooms for this period of time");
                return this.View(model);
            }
            return this.RedirectToAction("MyReservations", "Reservations");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.reservationsService.DoesReservationExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.MyReservations));
            }

            var model = await this.reservationsService.ReservationDetailsByIdAsync<EditReservationViewModel>(id);

            ApplicationUser guestId = await this.userManager.GetUserAsync(this.User);
            string guestEmail = guestId.Email;  // await this.guestsService.GetGuestEmailAsync(this.User);
            string guestPhoneNumber = guestId.PhoneNumber; // await this.guestsService.GetGuestPhoneNumberAsync(this.User);
            if (model.Email == string.Empty)
            {
                model.Email = guestEmail;
            }

            if (model.PhoneNumber == string.Empty)
            {
                model.PhoneNumber = guestPhoneNumber;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditReservationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO: Check if there are free rooms of this type left for this period of time. Write services. For Edit action also

            var applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.reservationsService.EditReservationAsync(model, id, applicationUserId);
                this.TempData["Message"] = "Reservation changed successfully.";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "You can't edit this reservation");
                return this.View(model);
            }

            return this.RedirectToAction("MyReservations", "Reservations");
        }

        public async Task<IActionResult> MyReservations(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int ReservationsPerPage = 10;
            var applicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new ReservationAllViewModel
            {
                ItemsPerPage = ReservationsPerPage,
                AllEntitiesCount = await this.reservationsService.GetCountOfMyReservationsAsync(applicationUserId),
                Reservations = await this.reservationsService.GetMyReservationsAsync<SingleReservationViewModel>(applicationUserId, id, ReservationsPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }
    }
}
