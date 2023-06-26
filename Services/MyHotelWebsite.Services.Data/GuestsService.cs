namespace MyHotelWebsite.Services.Data
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> guestsRepo; // NOT USED
        private readonly UserManager<ApplicationUser> userManager;

        public GuestsService(IDeletableEntityRepository<ApplicationUser> guestsRepo, UserManager<ApplicationUser> userManager)
        {
            this.guestsRepo = guestsRepo;
            this.userManager = userManager;
        }

        public async Task<int> GetCountAsync()
        {
            var guests = await this.userManager.GetUsersInRoleAsync("Guest");
            return guests.Count();
        }

        public async Task<string> GetGuestEmailAsync(ClaimsPrincipal claims)
        {
            ApplicationUser guestId = await this.userManager.GetUserAsync(claims);
            return guestId.Email;
        }

        public async Task<string> GetGuestPhoneNumberAsync(ClaimsPrincipal claims)
        {
            ApplicationUser guestID = await this.userManager.GetUserAsync(claims);
            return guestID.PhoneNumber;
        }
    }
}
