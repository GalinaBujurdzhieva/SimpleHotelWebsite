namespace MyHotelWebsite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomsRepo;

        public RoomsService(IDeletableEntityRepository<Room> roomsRepo)
        {
            this.roomsRepo = roomsRepo;
        }

        public async Task CleanRoomAsync(int id, string applicationUserId)
        {
            var currentRoom = await this.roomsRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            if (!currentRoom.IsCleaned)
            {
                currentRoom.IsCleaned = true;
                currentRoom.ApplicationUserId = applicationUserId;
            }
            else
            {
                throw new System.Exception();
            }

            await this.roomsRepo.SaveChangesAsync();
        }

        public async Task<bool> DoesRoomExistsAsync(int id)
        {
            return await this.roomsRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<int> GetAdultsCountAsync(int id)
        {
            RoomType roomType = await this.GetRoomTypeByIdAsync(id);

            if (roomType == RoomType.SingleRoom)
            {
                return 1;
            }
            else if (roomType == RoomType.DoubleRoom)
            {
                return 2;
            }
            else if (roomType == RoomType.Studio)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public async Task<IEnumerable<T>> GetAllFreeRoomsAtTheMomentAsync<T>()
        {
            var freeRoomsNow = await this.roomsRepo.AllAsNoTracking()
                .Include(x => x.RoomReservations)
                .ThenInclude(x => x.Reservation)
                .Where(x => !x.RoomReservations.Any(r => r.Reservation.AccommodationDate <= DateTime.UtcNow.Date &&
                r.Reservation.ReleaseDate > DateTime.UtcNow.Date))
                .OrderBy(x => x.RoomType)
                .ThenBy(x => x.Floor)
                .To<T>()
                .ToListAsync();
            return freeRoomsNow;
        }

        public async Task<IEnumerable<T>> GetAllFreeRoomsForACertainPeriodOfTimeAsync<T>(DateTime accommodationDate, DateTime releaseDate)
        {
            var freeRoomsForACertainPeriodOfTime = await this.roomsRepo.AllAsNoTracking()
                .Include(x => x.RoomReservations)
                .ThenInclude(x => x.Reservation)
                .Where(x => !x.RoomReservations.Any(r => r.Reservation.ReleaseDate.CompareTo(accommodationDate) >= 0 &&
                r.Reservation.ReleaseDate.CompareTo(releaseDate) > 0))
                .OrderBy(x => x.RoomType)
                .ThenBy(x => x.Floor)
                .To<T>()
                .ToListAsync();
            return freeRoomsForACertainPeriodOfTime;
        }

        public async Task<IEnumerable<T>> GetAllRoomsAsync<T>(int page, int itemsPerPage = 4)
        {
            var rooms = await this.roomsRepo.AllAsNoTracking()
                .OrderBy(x => x.RoomType)
                .ThenBy(x => x.Floor)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToListAsync();
            return rooms;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.roomsRepo.AllAsNoTracking().CountAsync();
        }

        public async Task<int> GetCountOfRoomsByFourCriteriaAsync(bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0)
        {
            var searchRoomsList = this.roomsRepo.AllAsNoTracking().AsQueryable();

            if (roomType != 0)
            {
                searchRoomsList = searchRoomsList.Where(x => x.RoomType == roomType);
            }

            if (isReserved)
            {
                searchRoomsList = searchRoomsList.Where(x => x.IsReserved);
            }

            if (isOccupied)
            {
                searchRoomsList = searchRoomsList.Where(x => x.IsOccupied);
            }

            if (isCleaned)
            {
                searchRoomsList = searchRoomsList.Where(x => x.IsCleaned);
            }

            return await searchRoomsList.CountAsync();
        }

        public async Task<RoomType> GetRoomTypeByIdAsync(int id)
        {
            var roomById = await this.roomsRepo.All().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (roomById == null)
            {
                throw new System.Exception();
            }

            return roomById.RoomType;
        }

        public async Task LeaveOccupiedRoomsAsync()
        {
            var roomsToBeLeft = await this.roomsRepo.All()
                .Include(x => x.RoomReservations)
                .ThenInclude(x => x.Reservation)
                .Where(x => !x.RoomReservations.Any(r => r.Reservation.AccommodationDate <= DateTime.UtcNow.Date &&
                r.Reservation.ReleaseDate > DateTime.UtcNow.Date))
                .ToListAsync();
            for (int i = 0; i < roomsToBeLeft.Count; i++)
            {
                roomsToBeLeft[i].IsOccupied = false;
            }

            await this.roomsRepo.SaveChangesAsync();
        }

        public async Task OccupyRoomsAsync()
        {
            var roomsToBeOccupied = await this.roomsRepo.All()
                .Include(x => x.RoomReservations)
                .ThenInclude(x => x.Reservation)
                .Where(x => x.RoomReservations.Any(r => r.Reservation.AccommodationDate.CompareTo(DateTime.UtcNow.Date) <= 0 && r.Reservation.ReleaseDate.CompareTo(DateTime.UtcNow.Date) > 0))
                .ToListAsync();
            foreach (var room in roomsToBeOccupied)
            {
                room.IsOccupied = true;
            }

            await this.roomsRepo.SaveChangesAsync();
        }

        public async Task RemoveIsReservedPropertyOfNotReservedRooms()
        {
            var notReservedRooms = await this.roomsRepo.All()
                .Include(x => x.RoomReservations)
                .ThenInclude(x => x.Reservation)
                .Where(x => !x.RoomReservations.Any(r => r.Reservation.ReleaseDate.CompareTo(DateTime.UtcNow.Date) > 0))
                .ToListAsync();
            foreach (var room in notReservedRooms)
            {
                room.IsReserved = false;
            }

            await this.roomsRepo.SaveChangesAsync();
        }

        public async Task<int> ReserveRoomAsync(RoomType roomType, DateTime accommodationDate, DateTime releaseDate)
        {
            var roomsThanCanBeReserved = await this.roomsRepo.All()
                .Include(r => r.RoomReservations)
                .ThenInclude(r => r.Reservation)
                .Where(r => r.RoomType == roomType)
                .Where(r => !r.RoomReservations.Any(r =>
                ((accommodationDate >= r.Reservation.AccommodationDate
                && accommodationDate <= r.Reservation.ReleaseDate)
                || (releaseDate >= r.Reservation.AccommodationDate
                && releaseDate <= r.Reservation.ReleaseDate))))
                .ToListAsync();

            if (roomsThanCanBeReserved.Count == 0)
            {
                throw new System.Exception();
            }

            var roomToBeReserved = roomsThanCanBeReserved.FirstOrDefault();

            if (roomToBeReserved != null)
            {
                roomToBeReserved.IsReserved = true;
            }

            await this.roomsRepo.SaveChangesAsync();
            return roomToBeReserved.Id;
        }

        public async Task<bool> ReserveRoomByIdAsync(int roomId, DateTime accommodationDate, DateTime releaseDate)
        {
            var roomToBeReserved = await this.roomsRepo.All()
                .Include(r => r.RoomReservations)
                .ThenInclude(r => r.Reservation)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (roomToBeReserved == null)
            {
                throw new System.Exception();
            }
            else
            {
                bool isRoomFreeForThisPeriodOfTime = !roomToBeReserved.RoomReservations.Any(r =>
                ((accommodationDate >= r.Reservation.AccommodationDate
                && accommodationDate <= r.Reservation.ReleaseDate)
                || (releaseDate >= r.Reservation.AccommodationDate
                && releaseDate <= r.Reservation.ReleaseDate)));

                if (isRoomFreeForThisPeriodOfTime)
                {
                    roomToBeReserved.IsReserved = true;
                    await this.roomsRepo.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }

        public async Task<IEnumerable<T>> SearchRoomsByFourCriteriaAsync<T>(int page, bool isReserved = false, bool isOccupied = false, bool isCleaned = false, RoomType roomType = 0, int itemsPerPage = 4)
        {
            var searchRoomsList = this.roomsRepo.AllAsNoTracking().AsQueryable();

            if (roomType != 0)
            {
                searchRoomsList = searchRoomsList.Where(x => x.RoomType == roomType);
            }

            if (isReserved)
            {
                searchRoomsList = searchRoomsList.Where(x => x.IsReserved);
            }

            if (isOccupied)
            {
                searchRoomsList = searchRoomsList.Where(x => x.IsOccupied);
            }

            if (isCleaned)
            {
                searchRoomsList = searchRoomsList.Where(x => x.IsCleaned);
            }

            return await searchRoomsList
                .OrderBy(x => x.RoomType)
                .ThenBy(x => x.Floor)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>().ToListAsync();
        }
    }
}
