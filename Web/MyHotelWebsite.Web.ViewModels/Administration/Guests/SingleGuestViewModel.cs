namespace MyHotelWebsite.Web.ViewModels.Administration.Guests
{
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;

    public class SingleGuestViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
