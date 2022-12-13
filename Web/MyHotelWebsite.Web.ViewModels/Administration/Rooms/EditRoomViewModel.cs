namespace MyHotelWebsite.Web.ViewModels.Administration.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;

    public class EditRoomViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ConvertValueInInvariantCulture = true)]
        public decimal AdultPrice { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ConvertValueInInvariantCulture = true)]
        public decimal ChildrenPrice { get; set; }

        public bool IsReserved { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsCleaned { get; set; }
    }
}
