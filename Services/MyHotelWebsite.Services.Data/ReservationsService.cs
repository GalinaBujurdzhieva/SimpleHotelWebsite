﻿namespace MyHotelWebsite.Services.Data
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
    using MyHotelWebsite.Web.ViewModels.Administration.Reservations;
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
                if (user.Email != model.ReservationEmail)
                {
                    user.ReservationEmails.Add(model.ReservationEmail);
                    reservation.ReservationEmail = model.ReservationEmail;
                }

                if (user.PhoneNumber != model.ReservationPhone)
                {
                    user.ReservationPhones.Add(model.ReservationPhone);
                    reservation.ReservationPhone = model.ReservationPhone;
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
            if (user.Email != model.ReservationEmail)
            {
                user.ReservationEmails.Add(model.ReservationEmail);
                currentReservation.ReservationEmail = model.ReservationEmail;
            }

            if (user.PhoneNumber != model.ReservationPhone)
            {
                user.ReservationPhones.Add(model.ReservationPhone);
                currentReservation.ReservationPhone = model.ReservationPhone;
            }

            await this.reservationsRepo.SaveChangesAsync();
            await this.roomsRepo.SaveChangesAsync();
        }

        public async Task<List<object>> FillPdf(int id)
        {
            var currentReservation = await this.reservationsRepo.All()
               .Include(r => r.RoomReservations)
               .ThenInclude(r => r.Room)
               .FirstOrDefaultAsync(x => x.Id == id);

            List<object> data = new List<object>();
            object row1 = new { ID = "Reservation", Name = "# " + currentReservation.Id };
            object row2 = new { ID = "Phone Number", Name = currentReservation.ReservationPhone };
            object row3 = new { ID = "Email", Name = currentReservation.ReservationEmail };
            object row4 = new { ID = "Accommodation Date", Name = currentReservation.AccommodationDate.ToString("dd/MM/yyyy") };
            object row5 = new { ID = "Release Date", Name = currentReservation.ReleaseDate.ToString("dd/MM/yyyy") };
            object row6 = new { ID = "Adults Count", Name = currentReservation.AdultsCount };
            object row7 = new { ID = "Children Count", Name = currentReservation.ChildrenCount ?? 0 };
            object row8 = new { ID = "Room Type", Name = currentReservation.RoomType };
            object row9 = new { ID = "Catering", Name = currentReservation.Catering };
            object row10 = new { ID = "Total Price", Name = currentReservation.TotalPrice.ToString("F2") + " euro"};

            data.Add(row1);
            data.Add(row2);
            data.Add(row3);
            data.Add(row4);
            data.Add(row5);
            data.Add(row6);
            data.Add(row7);
            data.Add(row8);
            data.Add(row9);
            data.Add(row10);
            return data;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.reservationsRepo.AllAsNoTracking().CountAsync();
        }

        public async Task<int> GetCountOfMyReservationsAsync(string applicationUserId)
        {
            return await this.reservationsRepo.AllAsNoTracking().Where(r => r.ApplicationUserId == applicationUserId).CountAsync();
        }

        public async Task<string> GetGuestEmail(int reservationId)
        {
            var currentReservation = await this.reservationsRepo.AllAsNoTracking().Where(r => r.Id == reservationId).FirstOrDefaultAsync();
            if (currentReservation != null)
            {
                string guestId = currentReservation.ApplicationUserId;
                ApplicationUser guest = await this.userManager.FindByIdAsync(guestId);
                return guest.Email;
            }

            return null;
        }

        public async Task<string> GetGuestPhoneNumber(int reservationId)
        {
            var currentReservation = await this.reservationsRepo.AllAsNoTracking().Where(r => r.Id == reservationId).FirstOrDefaultAsync();
            if (currentReservation != null)
            {
                string guestId = currentReservation.ApplicationUserId;
                ApplicationUser guest = await this.userManager.FindByIdAsync(guestId);
                return guest.PhoneNumber;
            }

            return null;
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

        public async Task HotelAdministrationCreateReservationAsync(HotelAdministrationAddReservationViewModel model)
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
                    TotalPrice = await this.GetReservationTotalPrice(model.RoomType, model.AccommodationDate, model.ReleaseDate, model.AdultsCount, model.ChildrenCount),
                    ReservationEmail = model.ReservationEmail,
                    ReservationPhone = model.ReservationPhone,
                };

                reservation.RoomReservations.Add(new RoomReservation()
                {
                    ReservationId = reservation.Id,
                    RoomId = await this.roomsService.ReserveRoomAsync(model.RoomType, model.AccommodationDate, model.ReleaseDate),
                });

                await this.reservationsRepo.AddAsync(reservation);
                await this.reservationsRepo.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw new System.Exception();
            }
        }

        public async Task HotelAdministrationEditReservationAsync(HotelAdministrationEditReservationViewModel model, int id)
        {
            var currentReservation = await this.reservationsRepo.All()
                .Include(r => r.RoomReservations)
                .ThenInclude(r => r.Room)
                .FirstOrDefaultAsync(x => x.Id == id);

            currentReservation.ReservationEmail = model.ReservationEmail;
            currentReservation.ReservationPhone = model.ReservationPhone;
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
                });
                currentReservation.RoomType = model.RoomType;
            }

            currentReservation.TotalPrice = await this.GetReservationTotalPrice(model.RoomType, model.AccommodationDate, model.ReleaseDate, model.AdultsCount, model.ChildrenCount);
            await this.reservationsRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> HotelAdministrationGetAllReservationsAsync<T>(int page, int itemsPerPage = 4)
        {
            var reservations = await this.reservationsRepo.AllAsNoTracking()
             .OrderByDescending(r => r.AccommodationDate)
             .ThenByDescending(r => r.ReleaseDate)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage).To<T>().ToListAsync();
            return reservations;
        }

        public async Task<T> HotelAdministrationReservationDetailsByIdAsync<T>(int id)
        {
            var currentReservation = await this.reservationsRepo.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            return currentReservation;
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
