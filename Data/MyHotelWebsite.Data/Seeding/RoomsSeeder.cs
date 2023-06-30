namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;

    internal class RoomsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Rooms.Any())
            {
                return;
            }

            List<Room> rooms = new List<Room>();

            // 1
            rooms.Add(new Room
            {
                RoomNumber = 1,
                Capacity = 1,
                Floor = 0,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 2
            rooms.Add(new Room
            {
                RoomNumber = 2,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 3
            rooms.Add(new Room
            {
                RoomNumber = 3,
                Capacity = 4,
                Floor = 0,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 4
            rooms.Add(new Room
            {
                RoomNumber = 4,
                Capacity = 3,
                Floor = 0,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 5
            rooms.Add(new Room
            {
                RoomNumber = 5,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 6
            rooms.Add(new Room
            {
                RoomNumber = 6,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 7
            rooms.Add(new Room
            {
                RoomNumber = 7,
                Capacity = 4,
                Floor = 0,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 8
            rooms.Add(new Room
            {
                RoomNumber = 8,
                Capacity = 4,
                Floor = 0,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 9
            rooms.Add(new Room
            {
                RoomNumber = 9,
                Capacity = 3,
                Floor = 0,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 10
            rooms.Add(new Room
            {
                RoomNumber = 10,
                Capacity = 3,
                Floor = 0,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 11
            rooms.Add(new Room
            {
                RoomNumber = 11,
                Capacity = 1,
                Floor = 1,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 12
            rooms.Add(new Room
            {
                RoomNumber = 12,
                Capacity = 1,
                Floor = 1,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 13
            rooms.Add(new Room
            {
                RoomNumber = 13,
                Capacity = 1,
                Floor = 1,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 14
            rooms.Add(new Room
            {
                RoomNumber = 14,
                Capacity = 2,
                Floor = 1,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 15
            rooms.Add(new Room
            {
                RoomNumber = 15,
                Capacity = 2,
                Floor = 1,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 16
            rooms.Add(new Room
            {
                RoomNumber = 16,
                Capacity = 2,
                Floor = 1,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 17
            rooms.Add(new Room
            {
                RoomNumber = 17,
                Capacity = 4,
                Floor = 1,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 18
            rooms.Add(new Room
            {
                RoomNumber = 18,
                Capacity = 4,
                Floor = 1,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 19
            rooms.Add(new Room
            {
                RoomNumber = 19,
                Capacity = 3,
                Floor = 1,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 20
            rooms.Add(new Room
            {
                RoomNumber = 20,
                Capacity = 3,
                Floor = 2,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 21
            rooms.Add(new Room
            {
                RoomNumber = 21,
                Capacity = 1,
                Floor = 2,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 22
            rooms.Add(new Room
            {
                RoomNumber = 22,
                Capacity = 1,
                Floor = 2,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 23
            rooms.Add(new Room
            {
                RoomNumber = 23,
                Capacity = 1,
                Floor = 2,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 24
            rooms.Add(new Room
            {
                RoomNumber = 24,
                Capacity = 2,
                Floor = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 25
            rooms.Add(new Room
            {
                RoomNumber = 25,
                Capacity = 2,
                Floor = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 26
            rooms.Add(new Room
            {
                RoomNumber = 26,
                Capacity = 2,
                Floor = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 27
            rooms.Add(new Room
            {
                RoomNumber = 27,
                Capacity = 4,
                Floor = 2,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 28
            rooms.Add(new Room
            {
                RoomNumber = 28,
                Capacity = 4,
                Floor = 2,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 29
            rooms.Add(new Room
            {
                RoomNumber = 29,
                Capacity = 4,
                Floor = 2,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 30
            rooms.Add(new Room
            {
                RoomNumber = 30,
                Capacity = 3,
                Floor = 2,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 31
            rooms.Add(new Room
            {
                RoomNumber = 31,
                Capacity = 1,
                Floor = 3,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 32
            rooms.Add(new Room
            {
                RoomNumber = 32,
                Capacity = 2,
                Floor = 3,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 33
            rooms.Add(new Room
            {
                RoomNumber = 33,
                Capacity = 2,
                Floor = 3,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 34
            rooms.Add(new Room
            {
                RoomNumber = 34,
                Capacity = 2,
                Floor = 3,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 35
            rooms.Add(new Room
            {
                RoomNumber = 35,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 36
            rooms.Add(new Room
            {
                RoomNumber = 36,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 37
            rooms.Add(new Room
            {
                RoomNumber = 37,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 38
            rooms.Add(new Room
            {
                RoomNumber = 38,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 39
            rooms.Add(new Room
            {
                RoomNumber = 39,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 40
            rooms.Add(new Room
            {
                RoomNumber = 40,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 41
            rooms.Add(new Room
            {
                RoomNumber = 41,
                Capacity = 1,
                Floor = 4,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 42
            rooms.Add(new Room
            {
                RoomNumber = 42,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 43
            rooms.Add(new Room
            {
                RoomNumber = 43,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 44
            rooms.Add(new Room
            {
                RoomNumber = 44,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 45
            rooms.Add(new Room
            {
                RoomNumber = 45,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 46
            rooms.Add(new Room
            {
                RoomNumber = 46,
                Capacity = 3,
                Floor = 4,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 47
            rooms.Add(new Room
            {
                RoomNumber = 47,
                Capacity = 3,
                Floor = 4,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 48
            rooms.Add(new Room
            {
                RoomNumber = 48,
                Capacity = 3,
                Floor = 4,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 49
            rooms.Add(new Room
            {
                RoomNumber = 49,
                Capacity = 4,
                Floor = 4,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 50
            rooms.Add(new Room
            {
                RoomNumber = 50,
                Capacity = 4,
                Floor = 4,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 51
            rooms.Add(new Room
            {
                RoomNumber = 51,
                Capacity = 1,
                Floor = 5,
                RoomType = RoomType.SingleRoom,
                AdultPrice = GlobalConstants.SingleRoomPrice,
                ChildrenPrice = GlobalConstants.SingleRoomPrice,
            });

            // 52
            rooms.Add(new Room
            {
                RoomNumber = 52,
                Capacity = 2,
                Floor = 5,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 53
            rooms.Add(new Room
            {
                RoomNumber = 53,
                Capacity = 2,
                Floor = 5,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 54
            rooms.Add(new Room
            {
                RoomNumber = 54,
                Capacity = 2,
                Floor = 5,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = GlobalConstants.DoubleRoomAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.DoubleRoomChildrenPricePerBed,
            });

            // 55
            rooms.Add(new Room
            {
                RoomNumber = 55,
                Capacity = 3,
                Floor = 5,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 56
            rooms.Add(new Room
            {
                RoomNumber = 56,
                Capacity = 3,
                Floor = 5,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 57
            rooms.Add(new Room
            {
                RoomNumber = 57,
                Capacity = 3,
                Floor = 5,
                RoomType = RoomType.Studio,
                AdultPrice = GlobalConstants.StudioAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.StudioChildrenPricePerBed,
            });

            // 58
            rooms.Add(new Room
            {
                RoomNumber = 58,
                Capacity = 4,
                Floor = 5,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 59
            rooms.Add(new Room
            {
                RoomNumber = 59,
                Capacity = 4,
                Floor = 5,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 60
            rooms.Add(new Room
            {
                RoomNumber = 60,
                Capacity = 4,
                Floor = 5,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 61
            rooms.Add(new Room
            {
                RoomNumber = 61,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 62
            rooms.Add(new Room
            {
                RoomNumber = 62,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 63
            rooms.Add(new Room
            {
                RoomNumber = 63,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 64
            rooms.Add(new Room
            {
                RoomNumber = 64,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 65
            rooms.Add(new Room
            {
                RoomNumber = 65,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            // 66
            rooms.Add(new Room
            {
                RoomNumber = 66,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = GlobalConstants.ApartmentAdultsPricePerBed,
                ChildrenPrice = GlobalConstants.ApartmentChildrenPricePerBed,
            });

            await dbContext.Rooms.AddRangeAsync(rooms);
            await dbContext.SaveChangesAsync();
        }
    }
}
