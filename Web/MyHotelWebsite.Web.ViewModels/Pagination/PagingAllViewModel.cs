namespace MyHotelWebsite.Web.ViewModels.Pagination
{
    using System;

    public class PagingAllViewModel
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
