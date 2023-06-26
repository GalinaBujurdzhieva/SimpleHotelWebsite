namespace MyHotelWebsite.Common.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AccomodationDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            if (DateTime.Compare(date, DateTime.UtcNow.Date) >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
