namespace MyHotelWebsite.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using MyHotelWebsite.Data.Models;

    public interface IGuestsService
    {
        Task<int> GetCountAsync();

        Task<string> GetGuestEmailAsync(ClaimsPrincipal claims);

        Task<string> GetGuestPhoneNumberAsync(ClaimsPrincipal claims);
    }
}
