namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;

    internal class ReservationsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Reservations.Any())
            {
                return;
            }

            List<Reservation> reservations = new List<Reservation>();

            // 1
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 1),
                    ReleaseDate = new DateTime(2023, 9, 10),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
                });

            // 2
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 1),
                    ReleaseDate = new DateTime(2023, 9, 5),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Without,
                    ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
                });

            // 3
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 1),
                    ReleaseDate = new DateTime(2023, 9, 7),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Breakfast,
                    ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
                });

            // 4
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 1),
                    ReleaseDate = new DateTime(2023, 9, 10),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Breakfast,
                    ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
                });

            // 5
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 11),
                    ReleaseDate = new DateTime(2023, 9, 20),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.AllInclusive,
                    ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
                });

            // 6
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 11),
                    ReleaseDate = new DateTime(2023, 9, 15),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.AllInclusive,
                    ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
                });

            // 7
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 21),
                    ReleaseDate = new DateTime(2023, 9, 30),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Dinner,
                    ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
                });

            // 8
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 17),
                    ReleaseDate = new DateTime(2023, 9, 18),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.Dinner,
                    ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
                });

            // 9
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 10, 1),
                    ReleaseDate = new DateTime(2023, 10, 7),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.BreakfastAndDinner,
                    ApplicationUserId = "f01486c4-0444-4471-9c6d-8ec3b1dbad5b",
                });

            // 10
            reservations.Add(
                new Reservation
                {
                    AccommodationDate = new DateTime(2023, 9, 20),
                    ReleaseDate = new DateTime(2023, 9, 25),
                    AdultsCount = 1,
                    RoomType = RoomType.SingleRoom,
                    Catering = Catering.BreakfastAndDinner,
                    ApplicationUserId = "32878df0-57cb-43e0-8da5-40ceb2c040a6",
                });

            await dbContext.Reservations.AddRangeAsync(reservations);
            await dbContext.SaveChangesAsync();
        }
    }
}
