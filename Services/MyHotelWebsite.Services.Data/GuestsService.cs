namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<Guest> guestsRepo;

        public GuestsService(IDeletableEntityRepository<Guest> guestsRepo)
        {
            this.guestsRepo = guestsRepo;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.guestsRepo.AllAsNoTracking().CountAsync();
        }
    }
}
