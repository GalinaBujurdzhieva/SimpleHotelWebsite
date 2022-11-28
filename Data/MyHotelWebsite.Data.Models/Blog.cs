﻿namespace MyHotelWebsite.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Common.Models;

    public class Blog : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(20000)]
        public string Content { get; set; }

        public string BlogImageUrl { get; set; }

        public string BlogImageId { get; set; }

        public virtual BlogImage BlogImage { get; set; }

        public string StaffId { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
