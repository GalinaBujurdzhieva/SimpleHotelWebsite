using MyHotelWebsite.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Data.Models
{
    public class Blog : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        public string Content { get; set; }

        public string BlogImageUrl { get; set; }

        [Required]
        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
