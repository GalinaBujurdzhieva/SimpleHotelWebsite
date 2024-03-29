﻿namespace MyHotelWebsite.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyHotelWebsite.Common;
    using MyHotelWebsite.Data.Common.Repositories;
    using MyHotelWebsite.Data.Models;
    using MyHotelWebsite.Services.Mapping;

    public class GuestsService : IGuestsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> guestsRepo;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public GuestsService(IDeletableEntityRepository<ApplicationUser> guestsRepo, IDeletableEntityRepository<ApplicationRole> rolesRepo, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.guestsRepo = guestsRepo;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.rolesRepo = rolesRepo;
        }

        public async Task<IEnumerable<T>> GetAllGuestsAsync<T>(int page, int itemsPerPage = 4)
        {
            var roles = await this.rolesRepo.AllAsNoTracking().ToListAsync();
            var guestRoleId = roles.Where(r => r.Name == GlobalConstants.GuestRoleName).FirstOrDefault().Id;

            var allGuestsList = await this.guestsRepo.AllAsNoTracking()
                .Include(g => g.Roles)
                .Where(g => g.Roles.Any(r => r.RoleId == guestRoleId))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToListAsync();
            return allGuestsList;
        }

        public async Task<int> GetCountAsync()
        {
            var guests = await this.userManager.GetUsersInRoleAsync(GlobalConstants.GuestRoleName);
            return guests.Count();
        }

        public async Task<string> GetGuestEmailAsync(ClaimsPrincipal claims)
        {
            ApplicationUser guestId = await this.userManager.GetUserAsync(claims);
            return guestId.Email;
        }

        public async Task<string> GetGuestPhoneNumberAsync(ClaimsPrincipal claims)
        {
            ApplicationUser guestID = await this.userManager.GetUserAsync(claims);
            return guestID.PhoneNumber;
        }
    }
}
