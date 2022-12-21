namespace MyHotelWebsite.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Common.CustomValidationAttributes;
    using MyHotelWebsite.Data.Models.Enums;

    public class AddReservationViewModel : IValidatableObject
    {
        [Display(Name = "Check in")]
        [DateTimeRange]
        public DateTime AccommodationDate { get; set; }

        [Display(Name = "Check out")]
        [DateTimeRange]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Adults Count")]
        [Range(1, 4)]
        public int AdultsCount { get; set; }

        [Display(Name = "Children Count")]
        [Range(0, 4)]
        public int ChildrenCount { get; set; }

        [Display(Name = "Room Type")]
        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        public Catering Catering { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ReleaseDate.CompareTo(this.AccommodationDate) < 0)
            {
                yield return new ValidationResult("Check in date can not be later than Check out date");
            }
        }
    }
}
