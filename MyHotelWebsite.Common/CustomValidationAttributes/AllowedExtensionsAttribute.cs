using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Common.CustomValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] allowedExtensions;

        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            this.allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           var file = value as IFormFile;

           if (file != null)
           {
                var extension = Path.GetExtension(file.FileName);
                if (!this.allowedExtensions.Contains(extension))
                {
                    return new ValidationResult("This photo extension is not allowed!");
                }
           }

           return ValidationResult.Success;
        }
    }
}
