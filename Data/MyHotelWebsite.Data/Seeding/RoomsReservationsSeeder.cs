using MyHotelWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelWebsite.Data.Seeding
{
    internal class RoomsReservationsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.RoomsReservations.Any())
            {
                return;
            }

            List<RoomReservation> roomsReservations = new List<RoomReservation>();

            // 1
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 1,
                RoomId = 1,
                ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
            });

            // 2
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 2,
                RoomId = 11,
                ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
            });

            // 3
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 3,
                RoomId = 12,
                ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
            });

            // 4
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 4,
                RoomId = 13,
                ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
            });

            // 5
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 5,
                RoomId = 21,
                ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
            });

            // 6
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 6,
                RoomId = 22,
                ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
            });

            // 7
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 7,
                RoomId = 23,
                ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
            });

            // 8
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 8,
                RoomId = 31,
                ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
            });

            // 9
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 9,
                RoomId = 41,
                ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
            });

            // 10
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 10,
                RoomId = 51,
                ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
            });

            await dbContext.RoomsReservations.AddRangeAsync(roomsReservations);
            await dbContext.SaveChangesAsync();
        }
    }
}
