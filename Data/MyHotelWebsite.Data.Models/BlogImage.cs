using MyHotelWebsite.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Data.Models
{
    public class BlogImage : BaseDeletableModel<int>
    {
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public string Extension { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
