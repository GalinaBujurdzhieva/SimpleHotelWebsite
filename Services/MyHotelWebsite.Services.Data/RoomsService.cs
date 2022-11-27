using Microsoft.EntityFrameworkCore;
using MyHotelWebsite.Data.Common.Repositories;
using MyHotelWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Services.Data
{
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
