﻿namespace MyHotelWebsite.Web.ViewModels.Administration.Blogs
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyHotelWebsite.Common.CustomValidationAttributes;

    public class CreateBlogViewModel
    {
        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Title { get; set; }

        [Required]
        [StringLength(20000, MinimumLength = 50, ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Content { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Selected file is not an image.")]
        public IFormFile Image { get; set; }

        public string BlogImageUrl { get; set; }
    }
}
