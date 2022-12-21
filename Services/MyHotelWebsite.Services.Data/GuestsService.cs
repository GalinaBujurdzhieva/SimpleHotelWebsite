namespace MyHotelWebsite.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> guestsRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public GuestsService(IDeletableEntityRepository<ApplicationUser> guestsRepo, UserManager<ApplicationUser> userManager)
        {
            this.guestsRepo = guestsRepo;
            this.userManager = userManager;
        }

        public async Task<int> GetCountAsync()
        {
            var usersInGuestRole = await this.userManager.GetUsersInRoleAsync("Guest");
            return usersInGuestRole.Count();
        }
    }
}
