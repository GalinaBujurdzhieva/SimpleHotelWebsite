namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> guestsRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public GuestsService(IDeletableEntityRepository<ApplicationUser> guestsRepo, UserManager<ApplicationUser> userManager)
        {
            this.guestsRepo = guestsRepo;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<T>> GetAllGuestsAsync<T>(int page, int itemsPerPage = 4)
        {
            var allGuestsList = await this.guestsRepo.AllAsNoTracking()
                .Include(g => g.Roles)
                .Where(g => g.Roles.Any(r => r.RoleId == "529f0271-23d7-431e-a2cc-726d552d2406"))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToListAsync();
            return allGuestsList;
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
