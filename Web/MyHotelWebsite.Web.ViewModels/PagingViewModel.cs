using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Web.ViewModels
{
    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public int AllEntitiesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public bool HasPreviousPage => this.PreviousPageNumber > 0;

        public bool HasNextPage => this.NextPageNumber < this.TotalPagesCount;

        public int TotalPagesCount => (int)Math.Ceiling((double)this.AllEntitiesCount / this.ItemsPerPage);
    }
}
