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
    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepo;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepo)
        {
            this.ordersRepo = ordersRepo;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.ordersRepo.AllAsNoTracking().CountAsync();
        }
    }
}
