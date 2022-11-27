using Microsoft.EntityFrameworkCore;
using MyHotelWebsite.Data.Common.Repositories;
using MyHotelWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Services.Data
{
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
