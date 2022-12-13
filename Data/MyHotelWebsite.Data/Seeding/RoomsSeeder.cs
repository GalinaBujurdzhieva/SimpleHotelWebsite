namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                AdultPrice = 60M,
                ChildrenPrice = 35M,
            });

            // 2
            rooms.Add(new Room
            {
                RoomNumber = 2,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 50M,
                ChildrenPrice = 30M,
            });

            // 3
            rooms.Add(new Room
            {
                RoomNumber = 3,
                Capacity = 4,
                Floor = 0,
                RoomType = RoomType.Apartment,
                AdultPrice = 70M,
                ChildrenPrice = 50M,
            });

            // 4
            rooms.Add(new Room
            {
                RoomNumber = 4,
                Capacity = 3,
                Floor = 0,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });

            // 5
            rooms.Add(new Room
            {
                RoomNumber = 5,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 50M,
                ChildrenPrice = 30M,
            });

            // 6
            rooms.Add(new Room
            {
                RoomNumber = 6,
                Capacity = 2,
                Floor = 0,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 50M,
                ChildrenPrice = 30M,
            });

            // 7
            rooms.Add(new Room
            {
                RoomNumber = 7,
                Capacity = 4,
                Floor = 0,
                RoomType = RoomType.Apartment,
                AdultPrice = 70M,
                ChildrenPrice = 50M,
            });

            // 8
            rooms.Add(new Room
            {
                RoomNumber = 8,
                Capacity = 4,
                Floor = 0,
                RoomType = RoomType.Apartment,
                AdultPrice = 70M,
                ChildrenPrice = 50M,
            });

            // 9
            rooms.Add(new Room
            {
                RoomNumber = 9,
                Capacity = 3,
                Floor = 0,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });

            // 10
            rooms.Add(new Room
            {
                RoomNumber = 10,
                Capacity = 3,
                Floor = 0,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });

            // 11
            rooms.Add(new Room
            {
                RoomNumber = 11,
                Capacity = 1,
                Floor = 1,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 62M,
                ChildrenPrice = 37M,
            });

            // 12
            rooms.Add(new Room
            {
                RoomNumber = 12,
                Capacity = 1,
                Floor = 1,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 62M,
                ChildrenPrice = 37M,
            });

            // 13
            rooms.Add(new Room
            {
                RoomNumber = 13,
                Capacity = 1,
                Floor = 1,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 62M,
                ChildrenPrice = 37M,
            });

            // 14
            rooms.Add(new Room
            {
                RoomNumber = 14,
                Capacity = 2,
                Floor = 1,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 53M,
                ChildrenPrice = 32M,
            });

            // 15
            rooms.Add(new Room
            {
                RoomNumber = 15,
                Capacity = 2,
                Floor = 1,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 53M,
                ChildrenPrice = 32M,
            });

            // 16
            rooms.Add(new Room
            {
                RoomNumber = 16,
                Capacity = 2,
                Floor = 1,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 53M,
                ChildrenPrice = 32M,
            });

            // 17
            rooms.Add(new Room
            {
                RoomNumber = 17,
                Capacity = 4,
                Floor = 1,
                RoomType = RoomType.Apartment,
                AdultPrice = 74M,
                ChildrenPrice = 51M,
            });

            // 18
            rooms.Add(new Room
            {
                RoomNumber = 18,
                Capacity = 4,
                Floor = 1,
                RoomType = RoomType.Apartment,
                AdultPrice = 74M,
                ChildrenPrice = 51M,
            });

            // 19
            rooms.Add(new Room
            {
                RoomNumber = 19,
                Capacity = 3,
                Floor = 1,
                RoomType = RoomType.Studio,
                AdultPrice = 67M,
                ChildrenPrice = 47M,
            });

            // 20
            rooms.Add(new Room
            {
                RoomNumber = 20,
                Capacity = 3,
                Floor = 2,
                RoomType = RoomType.Studio,
                AdultPrice = 67M,
                ChildrenPrice = 47M,
            });

            // 21
            rooms.Add(new Room
            {
                RoomNumber = 21,
                Capacity = 1,
                Floor = 2,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 61M,
                ChildrenPrice = 36M,
            });

            // 22
            rooms.Add(new Room
            {
                RoomNumber = 22,
                Capacity = 1,
                Floor = 2,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 61M,
                ChildrenPrice = 36M,
            });

            // 23
            rooms.Add(new Room
            {
                RoomNumber = 23,
                Capacity = 1,
                Floor = 2,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 61M,
                ChildrenPrice = 36M,
            });

            // 24
            rooms.Add(new Room
            {
                RoomNumber = 24,
                Capacity = 2,
                Floor = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 54M,
                ChildrenPrice = 33M,
            });

            // 25
            rooms.Add(new Room
            {
                RoomNumber = 25,
                Capacity = 2,
                Floor = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 54M,
                ChildrenPrice = 33M,
            });

            // 26
            rooms.Add(new Room
            {
                RoomNumber = 26,
                Capacity = 2,
                Floor = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 54M,
                ChildrenPrice = 33M,
            });

            // 27
            rooms.Add(new Room
            {
                RoomNumber = 27,
                Capacity = 4,
                Floor = 2,
                RoomType = RoomType.Apartment,
                AdultPrice = 76M,
                ChildrenPrice = 53M,
            });

            // 28
            rooms.Add(new Room
            {
                RoomNumber = 28,
                Capacity = 4,
                Floor = 2,
                RoomType = RoomType.Apartment,
                AdultPrice = 76M,
                ChildrenPrice = 53M,
            });

            // 29
            rooms.Add(new Room
            {
                RoomNumber = 29,
                Capacity = 4,
                Floor = 2,
                RoomType = RoomType.Apartment,
                AdultPrice = 76M,
                ChildrenPrice = 53M,
            });

            // 30
            rooms.Add(new Room
            {
                RoomNumber = 30,
                Capacity = 3,
                Floor = 2,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 31
            rooms.Add(new Room
            {
                RoomNumber = 31,
                Capacity = 1,
                Floor = 3,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 61M,
                ChildrenPrice = 36M,
            });

            // 32
            rooms.Add(new Room
            {
                RoomNumber = 32,
                Capacity = 2,
                Floor = 3,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 54M,
                ChildrenPrice = 33M,
            });

            // 33
            rooms.Add(new Room
            {
                RoomNumber = 33,
                Capacity = 2,
                Floor = 3,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 54M,
                ChildrenPrice = 33M,
            });

            // 34
            rooms.Add(new Room
            {
                RoomNumber = 34,
                Capacity = 2,
                Floor = 3,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 54M,
                ChildrenPrice = 33M,
            });

            // 35
            rooms.Add(new Room
            {
                RoomNumber = 35,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 36
            rooms.Add(new Room
            {
                RoomNumber = 36,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 37
            rooms.Add(new Room
            {
                RoomNumber = 37,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 38
            rooms.Add(new Room
            {
                RoomNumber = 38,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 39
            rooms.Add(new Room
            {
                RoomNumber = 39,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 40
            rooms.Add(new Room
            {
                RoomNumber = 40,
                Capacity = 3,
                Floor = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 69M,
                ChildrenPrice = 49M,
            });

            // 41
            rooms.Add(new Room
            {
                RoomNumber = 41,
                Capacity = 1,
                Floor = 4,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 59M,
                ChildrenPrice = 34M,
            });

            // 42
            rooms.Add(new Room
            {
                RoomNumber = 42,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 59M,
                ChildrenPrice = 34M,
            });

            // 43
            rooms.Add(new Room
            {
                RoomNumber = 43,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 53M,
                ChildrenPrice = 31M,
            });

            // 44
            rooms.Add(new Room
            {
                RoomNumber = 44,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 53M,
                ChildrenPrice = 31M,
            });

            // 45
            rooms.Add(new Room
            {
                RoomNumber = 45,
                Capacity = 2,
                Floor = 4,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 53M,
                ChildrenPrice = 31M,
            });

            // 46
            rooms.Add(new Room
            {
                RoomNumber = 46,
                Capacity = 3,
                Floor = 4,
                RoomType = RoomType.Studio,
                AdultPrice = 67M,
                ChildrenPrice = 47M,
            });

            // 47
            rooms.Add(new Room
            {
                RoomNumber = 47,
                Capacity = 3,
                Floor = 4,
                RoomType = RoomType.Studio,
                AdultPrice = 67M,
                ChildrenPrice = 47M,
            });

            // 48
            rooms.Add(new Room
            {
                RoomNumber = 48,
                Capacity = 3,
                Floor = 4,
                RoomType = RoomType.Studio,
                AdultPrice = 67M,
                ChildrenPrice = 47M,
            });

            // 49
            rooms.Add(new Room
            {
                RoomNumber = 49,
                Capacity = 4,
                Floor = 4,
                RoomType = RoomType.Apartment,
                AdultPrice = 74M,
                ChildrenPrice = 51M,
            });

            // 50
            rooms.Add(new Room
            {
                RoomNumber = 50,
                Capacity = 4,
                Floor = 4,
                RoomType = RoomType.Apartment,
                AdultPrice = 74M,
                ChildrenPrice = 51M,
            });

            // 51
            rooms.Add(new Room
            {
                RoomNumber = 51,
                Capacity = 1,
                Floor = 5,
                RoomType = RoomType.SingleRoom,
                AdultPrice = 57M,
                ChildrenPrice = 32M,
            });

            // 52
            rooms.Add(new Room
            {
                RoomNumber = 52,
                Capacity = 2,
                Floor = 5,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 51M,
                ChildrenPrice = 30M,
            });

            // 53
            rooms.Add(new Room
            {
                RoomNumber = 53,
                Capacity = 2,
                Floor = 5,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 51M,
                ChildrenPrice = 30M,
            });

            // 54
            rooms.Add(new Room
            {
                RoomNumber = 54,
                Capacity = 2,
                Floor = 5,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 51M,
                ChildrenPrice = 30M,
            });

            // 55
            rooms.Add(new Room
            {
                RoomNumber = 55,
                Capacity = 3,
                Floor = 5,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });

            // 56
            rooms.Add(new Room
            {
                RoomNumber = 56,
                Capacity = 3,
                Floor = 5,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });

            // 57
            rooms.Add(new Room
            {
                RoomNumber = 57,
                Capacity = 3,
                Floor = 5,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });

            // 58
            rooms.Add(new Room
            {
                RoomNumber = 58,
                Capacity = 4,
                Floor = 5,
                RoomType = RoomType.Apartment,
                AdultPrice = 72M,
                ChildrenPrice = 49M,
            });

            // 59
            rooms.Add(new Room
            {
                RoomNumber = 59,
                Capacity = 4,
                Floor = 5,
                RoomType = RoomType.Apartment,
                AdultPrice = 72M,
                ChildrenPrice = 49M,
            });

            // 60
            rooms.Add(new Room
            {
                RoomNumber = 60,
                Capacity = 4,
                Floor = 5,
                RoomType = RoomType.Apartment,
                AdultPrice = 72M,
                ChildrenPrice = 49M,
            });

            // 61
            rooms.Add(new Room
            {
                RoomNumber = 61,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = 90M,
                ChildrenPrice = 65M,
            });

            // 62
            rooms.Add(new Room
            {
                RoomNumber = 62,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = 90M,
                ChildrenPrice = 65M,
            });

            // 63
            rooms.Add(new Room
            {
                RoomNumber = 63,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = 90M,
                ChildrenPrice = 65M,
            });

            // 64
            rooms.Add(new Room
            {
                RoomNumber = 64,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = 90M,
                ChildrenPrice = 65M,
            });

            // 65
            rooms.Add(new Room
            {
                RoomNumber = 65,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = 90M,
                ChildrenPrice = 65M,
            });

            // 66
            rooms.Add(new Room
            {
                RoomNumber = 66,
                Capacity = 4,
                Floor = 6,
                RoomType = RoomType.Apartment,
                AdultPrice = 90M,
                ChildrenPrice = 65M,
            });

            await dbContext.Rooms.AddRangeAsync(rooms);
            await dbContext.SaveChangesAsync();
        }
    }
}
