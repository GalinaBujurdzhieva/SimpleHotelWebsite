namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Data.Models.Enums;
    using MyHotelWebsite.Services.Data;
    using MyHotelWebsite.Web.ViewModels.Administration.Guests;
    using MyHotelWebsite.Web.ViewModels.Administration.Orders;
    using MyHotelWebsite.Web.ViewModels.Administration.Staff;
    using MyHotelWebsite.Web.ViewModels.User;

    public class StaffController : AdministrationController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IStaffService staffService;

        public StaffController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IStaffService staffService)
        {
                this.signInManager = signInManager;
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.staffService = staffService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int StaffPerPage = 10;

            var model = new AllStaffViewModel
            {
                ItemsPerPage = StaffPerPage,
                AllEntitiesCount = await this.staffService.GetCountAsync(),
                Staff = await this.staffService.GetAllEmployeesAsync(id, StaffPerPage),
                PageNumber = id,
            };
            return this.View(model);
        }

        public async Task<IActionResult> ByRole(string role, int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            const int EmployeesPerPage = 10;

            var employeesByRole = await this.staffService.GetEmployeesByRoleAsync(id, role, EmployeesPerPage);
            var model = new StaffByRoleViewModel
            {
                ItemsPerPage = EmployeesPerPage,
                AllEntitiesCount = await this.staffService.GetCountOfEmployeesByRoleAsync(role),
                Staff = employeesByRole,
                PageNumber = id,
                Role = role,
            };
            return this.View(model);
        }

        public async Task<IActionResult> Lock(string id)
        {
            try
            {
                await this.staffService.LockUser(id);
                this.TempData["Message"] = "Successfully lock this employee.";
            }
            catch (System.Exception)
            {
                this.TempData["Error"] = "Employee already locked.";
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterStaffViewModel()
            {
                RoleList = this.roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i,
                }),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterStaffViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, model.Role);
                this.TempData["Message"] = "Employee created successfully.";
                return this.RedirectToAction(nameof(this.All));
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.View(model);
        }

        public IActionResult Search()
        {
            var model = new SingleStaffViewModel
            {
                RoleList = this.roleManager.Roles.Where(r => r.Name != GlobalConstants.GuestRoleName).Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i,
                }),
            };
            return this.View(model);
        }

        public async Task<IActionResult> Unlock(string id)
        {
            try
            {
                await this.staffService.UnlockUser(id);
                this.TempData["Message"] = "Successfully unlock this employee.";
            }
            catch (System.Exception)
            {
                this.TempData["Error"] = "Employee is not locked";
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
