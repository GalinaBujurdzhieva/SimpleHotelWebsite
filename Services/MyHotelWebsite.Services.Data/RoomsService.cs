namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomsRepo;

        public RoomsService(IDeletableEntityRepository<Room> roomsRepo)
        {
            this.roomsRepo = roomsRepo;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.roomsRepo.AllAsNoTracking().CountAsync();
        }
    }
}
