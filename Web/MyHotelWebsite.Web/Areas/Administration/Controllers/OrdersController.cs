namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Orders;
    using MyHotelWebsite.Web.ViewModels.Guests.Orders;
    using Syncfusion.Pdf;
    using Syncfusion.Pdf.Parsing;
    using Syncfusion.Pdf.Graphics;
    using Syncfusion.Pdf.Grid;
    using System.Data;
    using System.Xml.Linq;
    using System;
    using Syncfusion.Drawing;

    [Authorize(Roles = GlobalConstants.HotelManagerRoleName + ", " + GlobalConstants.ChefRoleName + ", " + GlobalConstants.WaiterRoleName)]
    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;
        private readonly IReservationsService reservationsService;

        public OrdersController(IOrdersService ordersService, IReservationsService reservationsService)
        {
                this.ordersService = ordersService;
                this.reservationsService = reservationsService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int OrdersPerPage = 10;

            var model = new HotelAdministrationAllOrderViewModel
            {
                ItemsPerPage = OrdersPerPage,
                AllEntitiesCount = await this.ordersService.GetCountAsync(),
                Orders = await this.ordersService.HotelAdministrationGetAllOrdersAsync<HotelAdministrationSingleOrderViewModel>(id, OrdersPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }

        public async Task<IActionResult> CreatePdfDocument(int id)
        {
            // Creates a new PDF document
            PdfDocument document = new PdfDocument();

            // Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;

            // Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfLayoutResult result = new PdfLayoutResult(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 95));
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            PdfGraphics g = page.Graphics;

            // Draw Rectangle place on location
            g.DrawRectangle(new PdfSolidBrush(new PdfColor(126, 151, 173)), new RectangleF(0, result.Bounds.Bottom + 40, g.ClientSize.Width, 30));
            var element = new PdfTextElement("INVOICE " + 10248, subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE " + DateTime.Now.ToString("MM/dd/yyyy");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            // Draw Bill header
            element = new PdfTextElement("BILL TO ", new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

            // Draw Bill address
            element = new PdfTextElement(string.Format("{0}, {1}, {2}", "Vin et alcohol Chevalier", "\n59 rue deb l'Abbaye ", " Reims, France"), new PdfStandardFont(PdfFontFamily.TimesRoman, 10));
            element.Brush = new PdfSolidBrush(new PdfColor(89, 89, 93));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, g.ClientSize.Width / 2, 100));

            // Draw Bill line
            g.DrawLine(new PdfPen(new PdfColor(126, 151, 173), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(g.ClientSize.Width, result.Bounds.Bottom + 3));

            // Creates the datasource for the table
            IEnumerable<object> invoiceDetails = await this.reservationsService.FillPdf(5);

            // Creates a PDF grid
            PdfGrid grid = new PdfGrid();

            // Adds the data source
            grid.DataSource = invoiceDetails;

            // Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];

            // Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            // Adds cell customizations
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

            // Applies the header style
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            // Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();

            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;

            // Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(g.ClientSize.Width, g.ClientSize.Height - 100)), layoutFormat);

            // Saves and closes the document.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            document.Close(true);
            string contentType = "application/pdf";
            string fileName = $"Order {id}.pdf";
            return this.File(stream, contentType, fileName);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.ordersService.DoesOrderExistsAsync(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.ordersService.DeleteOrderAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new AllDishesInOneOrderViewModel
            {
                Dishes = await this.ordersService.GetOrderDetailsAsync<SingleDishOrderViewModel>(id),
            };
            model.TotalPrice = this.ordersService.GetOrderTotalAsync(model.Dishes);
            return this.View(model);
        }
    }
}
