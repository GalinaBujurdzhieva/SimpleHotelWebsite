namespace MyHotelWebsite.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyHotelWebsite.Common;
    using MyHotelWebsite.Common.CustomValidationAttributes;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Mapping;

    public class AddReservationViewModel : IValidatableObject, IMapFrom<Reservation>
    {
        [NotMapped]
        public string Email { get; set; }

        [NotMapped]
        public string PhoneNumber { get; set; }

        [Display(Name = "Check in")]
        [Required]
        [DataType(DataType.Date)]
        [AccomodationDateValidation(ErrorMessage = "Check in date must be today or later")]
        public DateTime AccommodationDate { get; set; }

        [Display(Name = "Check out")]
        [Required]
        [DataType(DataType.Date)]
        [ReleaseDateValidation(ErrorMessage = "Check out date must be tomorrow or later")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Adults Count")]
        [Range(1, 4)]
        public int AdultsCount { get; set; }

        [Display(Name = "Children Count")]
        [Range(0, 4)]
        public int ChildrenCount { get; set; }

        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Some message")]
        public RoomType RoomType { get; set; }

        [Required]
        public Catering Catering { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (this.ReleaseDate <= this.AccommodationDate)
            {
                results.Add(new ValidationResult("Check out date must be later that Check In date", new[] { "ReleaseDate" }));
            }

            if (this.AdultsCount + this.ChildrenCount > 4)
            {
                results.Add(new ValidationResult("We do not have rooms for more than 4 people. Please make separate reservations or contact us to do this instead of you", new[] { "AdultsCount" }));
            }

            if (this.AdultsCount + this.ChildrenCount > (int)this.RoomType)
            {
                results.Add(new ValidationResult("Beds in the selected room are not enough", new[] { "RoomType" }));
            }

            return results;
        }
    }
}
