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
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepo;
        private readonly IDeletableEntityRepository<DishOrder> dishOrdersRepo;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepo, IDeletableEntityRepository<DishOrder> dishOrdersRepo)
        {
            this.ordersRepo = ordersRepo;
            this.dishOrdersRepo = dishOrdersRepo;
        }

        public async Task AddOrderAsync(AllShoppingCartsOfOneUserViewModel model, string applicationUserId)
        {
            try
            {
                Order newOrder = new Order
                {
                    ApplicationUserId = applicationUserId,
                    OrderStatus = OrderStatus.New,
                };
                foreach (var singleShoppingCart in model.ShoppingCartsList)
                {
                    newOrder.DishOrders.Add(new DishOrder()
                    {
                        OrderId = newOrder.Id,
                        ApplicationUserId = applicationUserId,
                        DishId = singleShoppingCart.DishId,
                        DishQuantity = singleShoppingCart.Count,
                    });
                }

                await this.ordersRepo.AddAsync(newOrder);
                await this.ordersRepo.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw new System.Exception();
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var currentOrder = await this.ordersRepo.All().FirstOrDefaultAsync(x => x.Id == id);
            var currentOrderDishes = await this.dishOrdersRepo.All()
               .Where(o => o.Order.Id == id)
               .ToListAsync();
            foreach (var orderDish in currentOrderDishes)
            {
                this.dishOrdersRepo.Delete(orderDish);
                await this.dishOrdersRepo.SaveChangesAsync();
            }

            this.ordersRepo.Delete(currentOrder);
            await this.ordersRepo.SaveChangesAsync();
        }

        public async Task<bool> DoesOrderExistsAsync(int id)
        {
            return await this.ordersRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await this.ordersRepo.AllAsNoTracking().Where(o => o.OrderStatus == OrderStatus.New).CountAsync();
        }

        public async Task<int> GetCountOfMyOrdersAsync(string applicationUserId)
        {
            return await this.ordersRepo.AllAsNoTracking().Where(r => r.ApplicationUserId == applicationUserId).CountAsync();
        }

        public async Task<IEnumerable<T>> GetMyOrdersAsync<T>(string applicationUserId, int page, int itemsPerPage = 4)
        {
            var orders = await this.ordersRepo.AllAsNoTracking()
              .Include(o => o.ApplicationUser)
             .Where(o => o.ApplicationUserId == applicationUserId)
             .OrderBy(o => o.CreatedOn)
             .ThenBy(o => o.OrderStatus)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage).To<T>().ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<T>> GetOrderDetailsAsync<T>(int id)
        {
            var currentOrderDishes = await this.dishOrdersRepo.AllAsNoTracking()
                .Include(o => o.Dish)
                .ThenInclude(d => d.DishImage)
                .Include(o => o.ApplicationUser)
                .Where(o => o.Order.Id == id)
                .To<T>()
                .ToListAsync();

            return currentOrderDishes;
        }

        public decimal GetOrderTotalAsync(IEnumerable<SingleDishOrderViewModel> dishesList)
        {
            decimal orderTotal = 0m;
            foreach (var dish in dishesList)
            {
                orderTotal += dish.TotalPrice;
            }

            return orderTotal;
        }

        public async Task<IEnumerable<T>> HotelAdministrationGetAllOrdersAsync<T>(int page, int itemsPerPage = 4)
        {
            var orders = await this.ordersRepo.AllAsNoTracking()
            .Include(o => o.ApplicationUser)
            .OrderBy(o => o.CreatedOn)
            .ThenBy(o => o.OrderStatus)
            .ThenBy(o => o.ApplicationUser.FirstName)
            .ThenBy(o => o.ApplicationUser.LastName)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage).To<T>().ToListAsync();
            return orders;
        }
    }
}
