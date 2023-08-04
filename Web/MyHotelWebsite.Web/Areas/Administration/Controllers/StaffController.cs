using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyHotelWebsite.Data.Models;
using MyHotelWebsite.Web.ViewModels.User;
using System.Threading.Tasks;

namespace MyHotelWebsite.Web.Areas.Administration.Controllers
{
    public class StaffController : AdministrationController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public StaffController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
                this.signInManager = signInManager;
                this.userManager = userManager;
                this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
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
                var guestRole = await this.roleManager.FindByNameAsync("Guest");
                if (guestRole != null)
                {
                    IdentityResult roleResult = await this.userManager.AddToRoleAsync(user, "Guest");
                }

                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.RedirectToAction("Login", "User");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.View(model);
        }
    }
}
