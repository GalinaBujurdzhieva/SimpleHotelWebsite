namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Administration.Staff;

    public interface IStaffService
    {
        Task<IEnumerable<SingleStaffViewModel>> GetAllEmployeesAsync(int page, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task LockUser(string id);

        Task UnlockUser(string id);
    }
}
