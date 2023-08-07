namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyHotelWebsite.Web.ViewModels.Administration.Staff;

    public interface IStaffService
    {
        Task<IEnumerable<SingleStaffViewModel>> GetAllEmployeesAsync(int page, int itemsPerPage = 4);

        Task<IEnumerable<SingleStaffViewModel>> GetEmployeesByRoleAsync(int page, string role, int itemsPerPage = 4);

        Task<int> GetCountAsync();

        Task<int> GetCountOfEmployeesByRoleAsync(string role);

        Task LockUser(string id);

        Task UnlockUser(string id);
    }
}
