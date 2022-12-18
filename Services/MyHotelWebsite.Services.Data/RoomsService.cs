namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;
    using MyHotelWebsite.Web.ViewModels.Administration.Rooms;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomsRepo;

        public RoomsService(IDeletableEntityRepository<Room> roomsRepo)
        {
            this.roomsRepo = roomsRepo;
        }

        public async Task CleanRoomAsync(int id, string staffId)
        {
            var currentRoom = await this.roomsRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            if (!currentRoom.IsCleaned)
            {
                currentRoom.IsCleaned = true;
                // currentRoom.StaffId = staffId;
            }
            else
            {
                throw new System.Exception();
            }

            await this.roomsRepo.SaveChangesAsync();
        }

        public async Task<bool> DoesRoomExistAsync(int id)
        {
            return await this.roomsRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task EditRoomAsync(EditRoomViewModel model, int id, string staffId)
        {
            var currentRoom = await this.roomsRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            currentRoom.AdultPrice = model.AdultPrice;
            currentRoom.ChildrenPrice = model.ChildrenPrice;
            currentRoom.IsReserved = model.IsReserved;
            currentRoom.IsOccupied = model.IsOccupied;
            currentRoom.IsCleaned = model.IsCleaned;
            // currentRoom.StaffId = staffId;

            await this.roomsRepo.SaveChangesAsync();
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

        public async Task<T> RoomDetailsByIdAsync<T>(int id)
        {
            var currentRoom = await this.roomsRepo.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            return currentRoom;
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
