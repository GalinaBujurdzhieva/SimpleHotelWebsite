using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyHotelWebsite.Data.Models.Enums;
using MyHotelWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyHotelWebsite.Common.CustomValidationAttributes;

namespace MyHotelWebsite.Web.ViewModels.Administration.Dishes
{
    public class CreateDishViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 0;

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        [Required]
        public string DishCategory { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Selected file is not an image.")]
        public IFormFile Image { get; set; }

        [Range(1, int.MaxValue)]
        public int QuantityInStock { get; set; }
    }
}
