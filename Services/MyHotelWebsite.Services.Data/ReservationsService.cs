namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Guests.Reservations;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationsRepo;
        private readonly IDeletableEntityRepository<Room> roomsRepo;
        private readonly IDeletableEntityRepository<RoomReservation> roomReservationRepo; // NOT USED
        private readonly IRoomsService roomsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationsRepo, IDeletableEntityRepository<RoomReservation> roomReservationRepo, IRoomsService roomsService, IDeletableEntityRepository<Room> roomsRepo, UserManager<ApplicationUser> userManager)
        {
            this.reservationsRepo = reservationsRepo;
            this.roomsService = roomsService;
            this.roomReservationRepo = roomReservationRepo;
            this.roomsRepo = roomsRepo;
            this.userManager = userManager;
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
                    TotalPrice = await this.GetReservationTotalPrice(model.RoomType, model.AccommodationDate, model.ReleaseDate, model.AdultsCount, model.ChildrenCount),
                };

                ApplicationUser user = await this.userManager.FindByIdAsync(applicationUserId);
                if (user.Email != model.Email)
                {
                    user.ReservationEmail = model.Email;
                }

                if (user.PhoneNumber != model.PhoneNumber)
                {
                    user.ReservationPhone = model.PhoneNumber;
                }

                reservation.RoomReservations.Add(new RoomReservation()
                {
                    ReservationId = reservation.Id,
                    RoomId = await this.roomsService.ReserveRoomAsync(model.RoomType, model.AccommodationDate, model.ReleaseDate),
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

        public Task DeleteReservationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesReservationExistsAsync(int id)
        {
            return await this.reservationsRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task EditReservationAsync(EditReservationViewModel model, int id, string applicationUserId)
        {
            var currentReservation = await this.reservationsRepo.All()
                .Include(r => r.RoomReservations)
                .ThenInclude(r => r.Room)
                .FirstOrDefaultAsync(x => x.Id == id);

            currentReservation.AccommodationDate = model.AccommodationDate;
            currentReservation.ReleaseDate = model.ReleaseDate;
            currentReservation.AdultsCount = model.AdultsCount;
            currentReservation.ChildrenCount = model.ChildrenCount;
            currentReservation.Catering = model.Catering;
            currentReservation.RoomType = model.RoomType;
            currentReservation.ApplicationUserId = applicationUserId;
            currentReservation.TotalPrice = await this.GetReservationTotalPrice(model.RoomType, model.AccommodationDate, model.ReleaseDate, model.AdultsCount, model.ChildrenCount);

            ApplicationUser user = await this.userManager.FindByIdAsync(applicationUserId);
            if (user.Email != model.Email)
            {
                user.ReservationEmail = model.Email;
            }

            if (user.PhoneNumber != model.PhoneNumber)
            {
                user.ReservationPhone = model.PhoneNumber;
            }

            currentReservation.RoomReservations.Add(new RoomReservation()
                {
                    ReservationId = currentReservation.Id,
                    RoomId = await this.roomsService.ReserveRoomAsync(model.RoomType, model.AccommodationDate, model.ReleaseDate),
                    ApplicationUserId = applicationUserId,
                });

            await this.reservationsRepo.SaveChangesAsync();
        }

        // USED
        public async Task<int> GetCountAsync()
        {
            return await this.reservationsRepo.AllAsNoTracking().CountAsync();
        }

        // USED
        public async Task<int> GetCountOfMyReservationsAsync(string applicationUserId)
        {
            return await this.reservationsRepo.AllAsNoTracking().Where(r => r.ApplicationUserId == applicationUserId).CountAsync();
        }

        // USED
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

        // USED
        public async Task<decimal> GetReservationTotalPrice(RoomType roomType, DateTime accomodationDate, DateTime releaseDate, int adultsCount, int childrenCount)
        {
            var room = await this.roomsRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(r => r.RoomType == roomType);

            TimeSpan totalAsTimeSpan = releaseDate.Subtract(accomodationDate);
            var totalDays = (int)totalAsTimeSpan.TotalDays;

            var totalPrice = totalDays * ((adultsCount * room.AdultPrice) + (childrenCount * room.ChildrenPrice));

            return totalPrice;
        }

        public async Task<T> ReservationDetailsByIdAsync<T>(int id)
        {
            var currentReservation = await this.reservationsRepo.AllAsNoTracking()
               .Include(r => r.ApplicationUser)
               .Where(x => x.Id == id)
               .To<T>()
               .FirstOrDefaultAsync();
            return currentReservation;
        }
    }
}
