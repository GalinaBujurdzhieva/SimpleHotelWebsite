namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationsRepo;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationsRepo)
        {
            this.reservationsRepo = reservationsRepo;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.reservationsRepo.AllAsNoTracking().CountAsync();
        }
    }
}
