namespace MyHotelWebsite.Web.ViewModels.Administration.Staff
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;

    public class SingleStaffViewModel : SingleGuestViewModel, IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string Role { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
