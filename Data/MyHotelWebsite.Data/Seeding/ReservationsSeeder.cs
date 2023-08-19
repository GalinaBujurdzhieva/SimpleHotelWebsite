namespace MyHotelWebsite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyHotelWebsite.Common;
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
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                    TotalPrice = 9 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                    TotalPrice = 4 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                    TotalPrice = 6 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                    TotalPrice = 9 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "e7522d06-694d-403b-86c7-175020363add",
                    TotalPrice = 9 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
                    TotalPrice = 4 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
                    TotalPrice = 9 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
                    TotalPrice = 1 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
                    TotalPrice = 6 * GlobalConstants.SingleRoomPrice,
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
                    ApplicationUserId = "3b537985-aa76-43b3-ae99-2ce17f4fab96",
                    TotalPrice = 4 * GlobalConstants.SingleRoomPrice,
                });

            await dbContext.Reservations.AddRangeAsync(reservations);
            await dbContext.SaveChangesAsync();
        }
    }
}
