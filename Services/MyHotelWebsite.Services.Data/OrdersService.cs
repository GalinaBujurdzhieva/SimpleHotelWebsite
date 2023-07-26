namespace MyHotelWebsite.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
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

        public async Task<int> GetCountAsync()
        {
            return await this.ordersRepo.AllAsNoTracking().CountAsync();
        }
    }
}
