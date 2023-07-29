namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Enums;
    using MyHotelWebsite.Web.ViewModels.Administration.Reservations;
    using Syncfusion.Pdf;
    using Syncfusion.Pdf.Grid;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.ReceptionistRoleName)]
    public class ReservationsController : AdministrationController
    {
        private readonly IRoomsService roomsService;
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationsController(IRoomsService roomsService, IReservationsService reservationsService, UserManager<ApplicationUser> userManager)
        {
            this.roomsService = roomsService;
            this.reservationsService = reservationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int ReservationsPerPage = 10;

            var model = new HotelAdministrationAllReservationViewModel
            {
                ItemsPerPage = ReservationsPerPage,
                AllEntitiesCount = await this.reservationsService.GetCountAsync(),
                Reservations = await this.reservationsService.HotelAdministrationGetAllReservationsAsync<HotelAdministrationSingleReservationViewModel>(id, ReservationsPerPage),
                PageNumber = id,
            };
            foreach (var singleModel in model.Reservations)
            {
                if (singleModel.ReservationEmail == null)
                {
                    singleModel.ReservationEmail = await this.reservationsService.GetGuestEmail(singleModel.Id);
                }

                if (singleModel.ReservationPhone == null)
                {
                    singleModel.ReservationPhone = await this.reservationsService.GetGuestPhoneNumber(singleModel.Id);
                }
            }

            return this.View(model);
        }

        public async Task<IActionResult> ByFiveCriteria(Catering catering, RoomType roomType, string reservationEmail, string reservationPhone, ReservationSorting sorting, int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int ReservationsPerPage = 15;

            var reservationsWithFilter = await this.reservationsService.HotelAdministrationGetReservationsByFiveCriteriaAsync<HotelAdministrationSingleReservationViewModel>(id, catering, roomType, sorting, reservationEmail, reservationPhone, ReservationsPerPage);
            var model = new HotelAdministrationReservationByFiveCriteriaViewModel
            {
                ItemsPerPage = ReservationsPerPage,
                AllEntitiesCount = await this.reservationsService.HotelAdministrationGetCountOfReservationsByFiveCriteriaAsync(reservationEmail, reservationPhone, catering, roomType, sorting),
                Reservations = reservationsWithFilter,
                PageNumber = id,
                Catering = catering.ToString(),
                RoomType = roomType.ToString(),
                ReservationEmail = reservationEmail,
                ReservationPhone = reservationPhone,
                Sorting = sorting,
            };
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new HotelAdministrationAddReservationViewModel()
            {
                AccommodationDate = DateTime.UtcNow,
                ReleaseDate = DateTime.UtcNow.AddDays(1),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotelAdministrationAddReservationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.reservationsService.HotelAdministrationCreateReservationAsync(model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "There are no free rooms for this period of time");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.reservationsService.DoesReservationExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.reservationsService.DeleteReservationAsync(id);
            await this.roomsService.RemoveIsReservedPropertyOfNotReservedRooms();
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.reservationsService.DoesReservationExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.reservationsService.HotelAdministrationReservationDetailsByIdAsync<HotelAdministrationSingleReservationViewModel>(id);
            if (model.ReservationEmail == null)
            {
                model.ReservationEmail = await this.reservationsService.GetGuestEmail(id);
            }

            if (model.ReservationPhone == null)
            {
                model.ReservationPhone = await this.reservationsService.GetGuestPhoneNumber(id);
            }

            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.reservationsService.DoesReservationExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.reservationsService.HotelAdministrationReservationDetailsByIdAsync<HotelAdministrationEditReservationViewModel>(id);
            if (model.ReservationEmail == null)
            {
                model.ReservationEmail = await this.reservationsService.GetGuestEmail(id);
            }

            if (model.ReservationPhone == null)
            {
                model.ReservationPhone = await this.reservationsService.GetGuestPhoneNumber(id);
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HotelAdministrationEditReservationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.reservationsService.HotelAdministrationEditReservationAsync(model, id);
                this.TempData["Message"] = "Reservation changed successfully.";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "You can't edit this reservation");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> CreatePdfDocument(int id)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            IEnumerable<object> dataTable = await this.reservationsService.FillPdf(id);
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            stream.Position = 0;
            doc.Close(true);
            string contentType = "application/pdf";
            string fileName = $"Reservation {id}.pdf";
            return this.File(stream, contentType, fileName);
        }

        public async Task<IActionResult> ReserveRoom(int id)
        {
            var model = new HotelAdministrationReserveRoomViewModel()
            {
                AccommodationDate = DateTime.UtcNow,
                ReleaseDate = DateTime.UtcNow.AddDays(1),
                RoomId = id,
                RoomType = await this.roomsService.GetRoomTypeByIdAsync(id),
                AdultsCount = await this.roomsService.GetAdultsCountAsync(id),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveRoom(HotelAdministrationReserveRoomViewModel model, int id)
        {
            model.RoomId = id;
            model.RoomType = await this.roomsService.GetRoomTypeByIdAsync(id);

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.reservationsService.HotelAdministrationReserveRoomAsync(model, id);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "The room is occupied for this period of time");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Search()
        {
            return this.View();
        }
    }
}
