using MyHotelWebsite.Data.Models;
using MyHotelWebsite.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Data.Seeding
{
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
                RoomType = RoomType.SingleRoom,
                AdultPrice = 60M,
                ChildrenPrice = 35M,
            });

            // 2
            rooms.Add(new Room
            {
                RoomNumber = 2,
                Capacity = 2,
                RoomType = RoomType.DoubleRoom,
                AdultPrice = 50M,
                ChildrenPrice = 30M,
            });

            // 3
            rooms.Add(new Room
            {
                RoomNumber = 3,
                Capacity = 4,
                RoomType = RoomType.Apartment,
                AdultPrice = 70M,
                ChildrenPrice = 50M,
            });

            rooms.Add(new Room
            {
                RoomNumber = 4,
                Capacity = 3,
                RoomType = RoomType.Studio,
                AdultPrice = 65M,
                ChildrenPrice = 45M,
            });
            await dbContext.Rooms.AddRangeAsync(rooms);
            await dbContext.SaveChangesAsync();
        }
    }
}
