namespace MyHotelWebsite.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Web.ViewModels.User;

    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
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

            public IActionResult Login()
        {
            var model = new LoginViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                var userIsInRoleAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.HotelManagerRoleName);
                var userIsInRoleChef = await this.userManager.IsInRoleAsync(user, GlobalConstants.ChefRoleName);
                var userIsInRoleWaiter = await this.userManager.IsInRoleAsync(user, GlobalConstants.WaiterRoleName);
                var userIsInRoleMaid = await this.userManager.IsInRoleAsync(user, GlobalConstants.MaidRoleName);
                var userIsInRoleReceptionist = await this.userManager.IsInRoleAsync(user, GlobalConstants.ReceptionistRoleName);
                var userIsInRoleWebsiteAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.WebsiteAdministratorRoleName);

                if (result.Succeeded)
                {
                    if (userIsInRoleAdmin || userIsInRoleChef || userIsInRoleWaiter || userIsInRoleMaid || userIsInRoleReceptionist || userIsInRoleWebsiteAdmin)
                    {
                        return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
                    }
                    else
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
