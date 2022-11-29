namespace MyHotelWebsite.Data.Models
{
    using System;

    using MyHotelWebsite.Data.Common.Models;

    public class BlogImage : BaseDeletableModel<string>
    {
        public BlogImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public string Extension { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
