namespace MyHotelWebsite.Web.ViewModels.Administration.Dishes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyHotelWebsite.Common.CustomValidationAttributes;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class EditDishViewModel : IMapFrom<Dish>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        [Required]
        public DishCategory DishCategory { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Selected file is not an image.")]
        public IFormFile Image { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        public string DishImageUrl { get; set; }
    }
}
