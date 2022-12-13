namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;

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
