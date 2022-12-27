namespace MyHotelWebsite.Common.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AccomodationDateValidationAttribute : ValidationAttribute
    {
        public AccomodationDateValidationAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            if (date.Day >= DateTime.UtcNow.Day)
            {
                return true;
            }

            return false;
        }
    }
}
