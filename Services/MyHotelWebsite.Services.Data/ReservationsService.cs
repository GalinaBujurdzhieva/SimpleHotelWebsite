namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Reservations;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationsRepo;
        private readonly IDeletableEntityRepository<Room> roomsRepo;
        private readonly IDeletableEntityRepository<RoomReservation> roomReservationRepo;
        private readonly IRoomsService roomsService;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationsRepo, IDeletableEntityRepository<RoomReservation> roomReservationRepo, IRoomsService roomsService, IDeletableEntityRepository<Room> roomsRepo)
        {
            this.reservationsRepo = reservationsRepo;
            this.roomsService = roomsService;
            this.roomReservationRepo = roomReservationRepo;
            this.roomsRepo = roomsRepo;
        }

        public async Task AddReservationAsync(AddReservationViewModel model, string applicationUserId)
        {
            try
            {
                var reservation = new Reservation
                {
                    AccommodationDate = model.AccommodationDate,
                    ReleaseDate = model.ReleaseDate,
                    AdultsCount = model.AdultsCount,
                    ChildrenCount = model.ChildrenCount,
                    RoomType = model.RoomType,
                    Catering = model.Catering,
                    ApplicationUserId = applicationUserId,
                    TotalPrice = await this.GetReservationTotalPrice(model),
                };
                reservation.RoomReservations.Add(new RoomReservation()
                {
                    ReservationId = reservation.Id,
                    RoomId = await this.roomsService.ReserveRoomAsync(model),
                    ApplicationUserId = applicationUserId,
                });

                await this.reservationsRepo.AddAsync(reservation);
                await this.reservationsRepo.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw new System.Exception();
            }
        }

        public async Task<int> GetCountAsync()
        {
            return await this.reservationsRepo.AllAsNoTracking().CountAsync();
        }

        public async Task<int> GetCountOfMyReservationsAsync(string applicationUserId)
        {
            return await this.reservationsRepo.AllAsNoTracking().Where(r => r.ApplicationUserId == applicationUserId).CountAsync();
        }

        public async Task<IEnumerable<T>> GetMyReservationsAsync<T>(string applicationUserId, int page, int itemsPerPage = 4)
        {
            var reservations = await this.reservationsRepo.AllAsNoTracking()
             .Where(r => r.ApplicationUserId == applicationUserId)
             .OrderBy(r => r.AccommodationDate)
             .ThenBy(r => r.ReleaseDate)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage).To<T>().ToListAsync();
            return reservations;
        }

        public async Task<decimal> GetReservationTotalPrice(AddReservationViewModel model)
        {
            var room = await this.roomsRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(r => r.RoomType == model.RoomType);

            TimeSpan totalAsTimeSpan = model.ReleaseDate.Subtract(model.AccommodationDate);
            var totalDays = (int)totalAsTimeSpan.TotalDays;

            var totalPrice = totalDays * ((model.AdultsCount * room.AdultPrice) + (model.ChildrenCount * room.ChildrenPrice));

            return (decimal)totalPrice;
        }
    }
}
