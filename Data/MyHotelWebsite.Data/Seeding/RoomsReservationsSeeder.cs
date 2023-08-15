namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;

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
                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
            });

            // 2
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 2,
                RoomId = 11,
                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
            });

            // 3
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 3,
                RoomId = 12,
                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
            });

            // 4
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 4,
                RoomId = 13,
                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
            });

            // 5
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 5,
                RoomId = 21,
                ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
            });

            // 6
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 6,
                RoomId = 22,
                ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
            });

            // 7
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 7,
                RoomId = 23,
                ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
            });

            // 8
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 8,
                RoomId = 31,
                ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
            });

            // 9
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 9,
                RoomId = 41,
                ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
            });

            // 10
            roomsReservations.Add(new RoomReservation()
            {
                ReservationId = 10,
                RoomId = 51,
                ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
            });

            await dbContext.RoomsReservations.AddRangeAsync(roomsReservations);
            await dbContext.SaveChangesAsync();
        }
    }
}
