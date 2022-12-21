namespace MyHotelWebsite.Common.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateTimeRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object date, ValidationContext validationContext)
        {
            date = (DateTime)date;
            return (DateTime.UtcNow.CompareTo(date) < 0 && DateTime.UtcNow.Day.CompareTo(date) > 365)
                  ? ValidationResult.Success
                  : new ValidationResult($"Invalid date range. It has to be no early than {DateTime.UtcNow.ToString("dd/MM/yyyy")} and no later than {DateTime.UtcNow.AddDays(365).ToString("dd/MM/yyyy")}");
        }
    }
}
