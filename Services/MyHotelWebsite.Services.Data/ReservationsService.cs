namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                    user.ReservationEmails.Add(model.Email);
                    reservation.ReservationEmail = model.Email;
                }

                if (user.PhoneNumber != model.PhoneNumber)
                {
                    user.ReservationPhones.Add(model.PhoneNumber);
                    reservation.ReservationPhone = model.PhoneNumber;
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

        public async Task DeleteReservationAsync(int id)
        {
            var currentReservation = await this.reservationsRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            var roomReservationToBeDeleted = await this.roomReservationRepo.All().FirstOrDefaultAsync(r => r.ReservationId == id);

            if (roomReservationToBeDeleted != null)
            {
                this.roomReservationRepo.Delete(roomReservationToBeDeleted);
            }

            this.reservationsRepo.Delete(currentReservation);

            await this.reservationsRepo.SaveChangesAsync();
            await this.roomReservationRepo.SaveChangesAsync();
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
            if (currentReservation.RoomType != model.RoomType)
            {
                var roomReservationToBeRemoved = currentReservation.RoomReservations.FirstOrDefault(r => r.ReservationId == id);
                var roomToBeFreed = roomReservationToBeRemoved.Room;
                if (this.roomReservationRepo.All().Where(r => r.RoomId == roomToBeFreed.Id).Count() == 1)
                {
                    roomToBeFreed.IsReserved = false;
                }

                currentReservation.RoomReservations.Remove(roomReservationToBeRemoved);
                currentReservation.RoomReservations.Add(new RoomReservation()
                {
                    ReservationId = currentReservation.Id,
                    RoomId = await this.roomsService.ReserveRoomAsync(model.RoomType, model.AccommodationDate, model.ReleaseDate),
                    ApplicationUserId = applicationUserId,
                });
                currentReservation.RoomType = model.RoomType;
            }

            currentReservation.ApplicationUserId = applicationUserId;
            currentReservation.TotalPrice = await this.GetReservationTotalPrice(model.RoomType, model.AccommodationDate, model.ReleaseDate, model.AdultsCount, model.ChildrenCount);

            ApplicationUser user = await this.userManager.FindByIdAsync(applicationUserId);
            if (user.Email != model.Email)
            {
                user.ReservationEmails.Add(model.Email);
                currentReservation.ReservationEmail = model.Email;
            }

            if (user.PhoneNumber != model.PhoneNumber)
            {
                user.ReservationPhones.Add(model.PhoneNumber);
                currentReservation.ReservationPhone = model.PhoneNumber;
            }

            await this.reservationsRepo.SaveChangesAsync();
            await this.roomsRepo.SaveChangesAsync();
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

        public async Task<decimal> GetReservationTotalPrice(RoomType roomType, DateTime accomodationDate, DateTime releaseDate, int adultsCount, int childrenCount)
        {
            var room = await this.roomsRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(r => r.RoomType == roomType);

            TimeSpan totalAsTimeSpan = releaseDate.Subtract(accomodationDate);
            var totalDays = (int)totalAsTimeSpan.TotalDays;

            var totalPrice = totalDays * ((adultsCount * room.AdultPrice) + (childrenCount * room.ChildrenPrice));

            return totalPrice;
        }

        public async Task<bool> IsReservationActiveAtTheMoment(int id)
        {
            var currentReservation = await this.reservationsRepo.AllAsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (currentReservation.AccommodationDate.CompareTo(DateTime.UtcNow.Date) <= 0 && currentReservation.ReleaseDate.CompareTo(DateTime.UtcNow.Date) > 0)
            {
                return true;
            }

            return false;
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
