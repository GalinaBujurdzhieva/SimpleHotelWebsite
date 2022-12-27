namespace MyHotelWebsite.Common.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReleaseDateValidationAttribute : ValidationAttribute
    {
        public ReleaseDateValidationAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            if (date.Day >= DateTime.UtcNow.AddDays(1).Day)
            {
                return true;
            }

            return false;
        }
    }
}
