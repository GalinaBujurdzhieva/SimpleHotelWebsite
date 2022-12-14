namespace MyHotelWebsite.Web.Areas.Administration.Controllers
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

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, EditRoomViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }
        //    var staffId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    //try
        //    //{
        //    //    await this.roomsService.EditRoomAsync(model, id, staffId, $"{this.environment.WebRootPath}/images");
        //    //    this.TempData["Message"] = "Dish changed successfully.";
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    this.ModelState.AddModelError(string.Empty, "Could not edit this dish");
        //    //    return this.View(model);
        //    //}

        //    return this.RedirectToAction(nameof(this.All));
        //}

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
