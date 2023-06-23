namespace MyHotelWebsite.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum RoomType
    {
        [Display(Name = "Single Room (1 bed)")]
        SingleRoom = 1,

        [Display(Name = "Double Room (2 beds)")]
        DoubleRoom = 2,

        [Display(Name = "Studio (3 beds)")]
        Studio = 3,

        [Display(Name = "Apartment (4 beds)")]
        Apartment = 4,
    }
}
