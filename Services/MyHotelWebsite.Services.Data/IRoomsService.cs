﻿namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoomsService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetAllRoomsAsync<T>(int page, int itemsPerPage = 4);
    }
}
