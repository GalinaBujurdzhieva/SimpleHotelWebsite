namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;

    public interface IGuestsService
    {
        Task<IEnumerable<T>> GetAllGuestsAsync<T>(int page, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task<string> GetGuestEmailAsync(ClaimsPrincipal claims);

        Task<string> GetGuestPhoneNumberAsync(ClaimsPrincipal claims);
    }
}
