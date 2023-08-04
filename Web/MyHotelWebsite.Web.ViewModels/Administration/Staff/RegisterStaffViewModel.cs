namespace MyHotelWebsite.Web.ViewModels.Administration.Staff
{
    using System.Collections.Generic;

    //using Microsoft.AspNetCore.Mvc.Rendering;
    using MyHotelWebsite.Web.ViewModels.User;

    public class RegisterStaffViewModel : RegisterViewModel
    {
        public string Role { get; set; }

        //public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
