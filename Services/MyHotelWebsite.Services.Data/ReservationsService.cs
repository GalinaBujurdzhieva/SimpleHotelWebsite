namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Web.ViewModels.Reservations;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationsRepo;
        private readonly IRoomsService roomsService;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationsRepo, IRoomsService roomsService)
        {
            this.reservationsRepo = reservationsRepo;
            this.roomsService = roomsService;
        }

        public async Task AddReservationAsync(AddReservationViewModel model, string applicationUserId)
        {
            
        }

        public async Task<int> GetCountAsync()
        {
            return await this.reservationsRepo.AllAsNoTracking().CountAsync();
        }
    }
}
