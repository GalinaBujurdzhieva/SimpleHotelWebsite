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
    using MyHotelWebsite.Web.ViewModels.Administration.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.ShoppingCarts;
    using Syncfusion.Drawing;
    using Syncfusion.Pdf;
    using Syncfusion.Pdf.Graphics;
    using Syncfusion.Pdf.Grid;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepo;
        private readonly IDeletableEntityRepository<DishOrder> dishOrdersRepo;
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartsRepo;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepo, IDeletableEntityRepository<DishOrder> dishOrdersRepo, IDeletableEntityRepository<ShoppingCart> shoppingCartsRepo)
        {
            this.ordersRepo = ordersRepo;
            this.dishOrdersRepo = dishOrdersRepo;
            this.shoppingCartsRepo = shoppingCartsRepo;
        }

        public async Task AddCommentToOrderAsync(int id, string comment)
        {
            var currentOrder = await this.ordersRepo.All().FirstOrDefaultAsync(o => o.Id == id);
            currentOrder.Comment = comment;
            await this.ordersRepo.SaveChangesAsync();
        }

        public async Task AddOrderAsync(AllShoppingCartsOfOneUserViewModel model, string applicationUserId)
        {
            if (model.ShoppingCartsList.Count > 0)
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
                    var currentShoppingCart = await this.shoppingCartsRepo.All().FirstOrDefaultAsync(sh => sh.Id == singleShoppingCart.Id);
                    currentShoppingCart.IsShoppingCartAddedToAFinalOrder = true;
                }

                await this.ordersRepo.AddAsync(newOrder);
                await this.ordersRepo.SaveChangesAsync();
                await this.dishOrdersRepo.SaveChangesAsync();
            }
            else
            {
                throw new System.Exception();
            }
        }

        public async Task ChangeOrderStatusWhenAllDishesAreReady()
        {
             var newOrders = await this.ordersRepo.All()
                .Include(o => o.DishOrders)
                .ThenInclude(o => o.Dish)
                .Where(o => o.OrderStatus == OrderStatus.New || o.OrderStatus == OrderStatus.InProgress)
                .ToListAsync();
             foreach (var newOrder in newOrders)
            {
                var dishOrders = newOrder.DishOrders.ToList();
                bool orderIsReady = dishOrders.All(d => d.Dish.IsReady == true);
                if (orderIsReady)
                {
                    newOrder.OrderStatus = OrderStatus.Ready;
                    foreach (var dishOrder in dishOrders)
                    {
                        dishOrder.Dish.QuantityInStock = dishOrder.Dish.QuantityInStock -= dishOrder.DishQuantity;
                        if (dishOrder.Dish.QuantityInStock <= 0)
                        {
                            dishOrder.Dish.QuantityInStock = 0;
                        }

                        await this.dishOrdersRepo.SaveChangesAsync();
                    }
                }
                else
                {
                    newOrder.OrderStatus = OrderStatus.InProgress;
                }

                await this.ordersRepo.SaveChangesAsync();
            }
        }

        public async Task ChangeStatusOfOrderAsync(int id, OrderStatus orderStatus)
        {
            var currentOrder = await this.ordersRepo.All()
                .Include(d => d.DishOrders)
                .ThenInclude(d => d.Dish)
                .FirstOrDefaultAsync(o => o.Id == id);
            currentOrder.OrderStatus = orderStatus;
            if (orderStatus == OrderStatus.TakenToTheGuest)
            {
                var currentOrderDishes = await this.dishOrdersRepo.All()
               .Include(o => o.Dish)
               .Where(o => o.Order.Id == id)
               .ToListAsync();
                foreach (var dishOrder in currentOrderDishes)
                {
                    dishOrder.Dish.IsReady = false;
                }
            }

            await this.dishOrdersRepo.SaveChangesAsync();
            await this.ordersRepo.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var currentOrder = await this.ordersRepo.All()
                .Include(d => d.DishOrders)
                .ThenInclude(d => d.Dish)
                .FirstOrDefaultAsync(d => d.Id == id);
            var currentOrderDishes = await this.dishOrdersRepo.All()
               .Include(o => o.Dish)
               .Where(o => o.Order.Id == id)
               .ToListAsync();
            foreach (var dishOrder in currentOrderDishes)
            {
                if (dishOrder.Dish.IsReady == true)
                {
                    dishOrder.Dish.QuantityInStock += dishOrder.DishQuantity;
                }
            }

            foreach (var orderDish in currentOrderDishes)
            {
                this.dishOrdersRepo.Delete(orderDish);
            }

            await this.dishOrdersRepo.SaveChangesAsync();
            this.ordersRepo.Delete(currentOrder);
            await this.ordersRepo.SaveChangesAsync();
        }

        public async Task<bool> DoesOrderExistsAsync(int id)
        {
            return await this.ordersRepo.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<PdfDocument> FillPdfOrderAsync(int id)
        {
            var currentOrder = await this.ordersRepo.All()
                .Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == id);

            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;
            PdfPage page = document.Pages.Add();
            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 95));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            PdfGraphics g = page.Graphics;
            g.DrawRectangle(new PdfSolidBrush(new PdfColor(255, 65, 87)), new RectangleF(0, result.Bounds.Bottom + 40, g.ClientSize.Width, 30));

            IEnumerable<SingleDishOrderViewModel> currentOrderDishes = await this.GetOrderDetailsAsync<SingleDishOrderViewModel>(id);
            decimal currentOrderTotal = this.GetOrderTotalAsync(currentOrderDishes);

            var element = new PdfTextElement($"ORDER #" + id + ", STATUS: " + currentOrder.OrderStatus.ToString() + ", TOTAL: " + currentOrderTotal + " euro", subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = $"DATE " + currentOrder.CreatedOn.ToShortDateString();
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            element = new PdfTextElement("GUEST: ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(255, 65, 87));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            string userName = currentOrder.ApplicationUser.FirstName + " " + currentOrder.ApplicationUser.LastName;
            string userPhoneNumber = currentOrder.ApplicationUser.PhoneNumber;
            string comment = currentOrder.Comment;
            if (string.IsNullOrEmpty(comment))
            {
                comment = "No comments";
            }

            element = new PdfTextElement(string.Format("{0}, {1}, {2}", userName, $"\n{userPhoneNumber}", comment), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, g.ClientSize.Width / 2, 100));
            g.DrawLine(new PdfPen(new PdfColor(255, 65, 87), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(g.ClientSize.Width, result.Bounds.Bottom + 3));

            IEnumerable<object> orderDetails = await this.FillPdfTableWithDishes(id);
            PdfGrid grid = new PdfGrid();
            grid.DataSource = orderDetails;
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(255, 65, 87));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(255, 65, 87));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                {
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                }
                else
                {
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }
            }

            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(g.ClientSize.Width, g.ClientSize.Height - 100)), layoutFormat);
            return document;
        }

        public async Task<List<object>> FillPdfTableWithDishes(int id)
        {
            IEnumerable<SingleDishOrderViewModel> currentOrderDishes = await this.GetOrderDetailsAsync<SingleDishOrderViewModel>(id);
            List<object> data = new List<object>();

            foreach (var dishOrder in currentOrderDishes)
            {
                object row1 = new { ID = "Dish Name", Name = dishOrder.Dish.Name };
                object row2 = new { ID = "Dish Category", Name = dishOrder.Dish.DishCategory };
                object row3 = new { ID = "Is Dish Ready", Name = dishOrder.Dish.IsReady ? "Yes" : "No" };
                object row4 = new { ID = "Price (euro)", Name = dishOrder.Dish.Price.ToString("F2") };
                object row5 = new { ID = "Quantity", Name = dishOrder.DishQuantity };
                object row6 = new { ID = "Total (euro)", Name = dishOrder.Dish.Price * dishOrder.DishQuantity };
                object row7 = new { ID = string.Empty, Name = string.Empty };

                data.Add(row1);
                data.Add(row2);
                data.Add(row3);
                data.Add(row4);
                data.Add(row5);
                data.Add(row6);
                data.Add(row7);
            }

            return data;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.ordersRepo.AllAsNoTracking().Where(o => o.OrderStatus == OrderStatus.New).CountAsync();
        }

        public async Task<int> GetCountOfMyOrdersAsync(string applicationUserId)
        {
            return await this.ordersRepo.AllAsNoTracking().Where(r => r.ApplicationUserId == applicationUserId).CountAsync();
        }

        public async Task<int> GetCountOfOrdersByOrderStatusAsync(OrderStatus orderStatus)
        {
            var searchOrdersList = this.ordersRepo.AllAsNoTracking().AsQueryable();
            if (orderStatus != 0)
            {
                searchOrdersList = searchOrdersList
                    .Where(x => x.OrderStatus == orderStatus);
            }

            return await searchOrdersList.CountAsync();
        }

        public async Task<IEnumerable<T>> GetMyOrdersAsync<T>(string applicationUserId, int page, int itemsPerPage = 4)
        {
            var orders = await this.ordersRepo.AllAsNoTracking()
              .Include(o => o.ApplicationUser)
             .Where(o => o.ApplicationUserId == applicationUserId)
             .OrderByDescending(o => o.CreatedOn)
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

        public async Task<HotelAdministrationAddCommentToOrderViewModel> GetOrderDetailsToAddCommentAsync(int id)
        {
            HotelAdministrationAddCommentToOrderViewModel currentOrder = await this.ordersRepo.AllAsNoTracking()
                .Include(o => o.ApplicationUser)
                .Where(o => o.Id == id)
                .To<HotelAdministrationAddCommentToOrderViewModel>()
                .FirstOrDefaultAsync();

            return currentOrder;
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
            .OrderByDescending(o => o.CreatedOn)
            .ThenBy(o => o.OrderStatus)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage).To<T>().ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<T>> HotelAdministrationGetOrdersByOrderStatusAsync<T>(int page, OrderStatus orderStatus, int itemsPerPage = 4)
        {
            var ordersByOrderStatus = this.ordersRepo.AllAsNoTracking().AsQueryable();

            if (orderStatus != 0)
            {
                ordersByOrderStatus = ordersByOrderStatus
                    .Where(x => x.OrderStatus == orderStatus);
            }

            return await ordersByOrderStatus.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToListAsync();
        }
    }
}
