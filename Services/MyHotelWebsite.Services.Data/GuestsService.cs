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
    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<Guest> guestsRepo;

        public GuestsService(IDeletableEntityRepository<Guest> guestsRepo)
        {
            this.guestsRepo = guestsRepo;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.guestsRepo.AllAsNoTracking().CountAsync();
        }
    }
}
