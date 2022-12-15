﻿namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.MaidRoleName)]
    public class RoomsController : AdministrationController
    {
        private readonly IRoomsService roomsService;

        public RoomsController(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int RoomsPerPage = 12;
            var model = new RoomAllViewModel
            {
                ItemsPerPage = RoomsPerPage,
                AllEntitiesCount = await this.roomsService.GetCountAsync(),
                Rooms = await this.roomsService.GetAllRoomsAsync<SingleRoomViewModel>(id, RoomsPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }

        public async Task<IActionResult> Clean(int id)
        {
            if (!await this.roomsService.DoesRoomExistAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this.roomsService.CleanRoomAsync(id, staffId);
                this.TempData["Message"] = "Room cleaned successfully.";
            }
            catch (Exception)
            {
                this.TempData["Message"] = "Room is already cleaned.";
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.roomsService.DoesRoomExistAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.roomsService.RoomDetailsByIdAsync<EditRoomViewModel>(id);
            this.DropDownReBind();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRoomViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this.roomsService.EditRoomAsync(model, id, staffId);
                this.TempData["Message"] = "Room edited successfully.";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Could not edit this room");
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Search()
        {
            this.DropDownReBind();
            return this.View();
        }

        private void DropDownReBind()
        {
            List<SelectListItem> boolYesOrNo = new List<SelectListItem>();

            boolYesOrNo.Add(new SelectListItem
            {
                Text = "Yes",
                Value = true.ToString(),
            });
            boolYesOrNo.Add(new SelectListItem
            {
                Text = "No",
                Value = false.ToString(),
            });

            this.ViewData["boolYesOrNo"] = boolYesOrNo;
        }
    }
}
