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
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Reservations;
    using Syncfusion.Pdf.Grid;
    using Syncfusion.Pdf;

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

        public async Task <IActionResult> CreatePdfDocument(int id)
        {
            // Generate a new PDF document.
            PdfDocument doc = new PdfDocument();

            // Add a page.
            PdfPage page = doc.Pages.Add();

            // Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            // Add list to IEnumerable.
            IEnumerable<object> dataTable = await this.reservationsService.FillPdf(id);

            // Assign data source.
            pdfGrid.DataSource = dataTable;

            // Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));

            // Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);

            // If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;

            // Close the document.
            doc.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";

            // Define the file name.
            string fileName = "Reservation.pdf";

            // Creates a FileContentResult object by using the file contents, content type, and file name.
            return this.File(stream, contentType, fileName);
        }

        // ??? NOT IMPLEMENTED
        public async Task<IActionResult> ReserveRoom(int id)
        {
            var model = new HotelAdministrationAddReservationViewModel()
            {
                AccommodationDate = DateTime.UtcNow,
                ReleaseDate = DateTime.UtcNow.AddDays(1),
                RoomType = await this.roomsService.GetRoomTypeByIdAsync(id),
                AdultsCount = await this.roomsService.GetAdultsCountAsync(id),
            };
            return this.View(model);
        }

        // ??? NOT IMPLEMENTED
        [HttpPost]
        public async Task<IActionResult> ReserveRoom(HotelAdministrationAddReservationViewModel model)
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
    }
}
