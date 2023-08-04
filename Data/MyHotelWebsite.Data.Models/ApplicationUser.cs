// ReSharper disable VirtualMemberCallInConstructor
namespace MyHotelWebsite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;
    using MyHotelWebsite.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Orders = new HashSet<Order>();
            this.Reservations = new HashSet<Reservation>();
            this.Rooms = new HashSet<Room>();
            this.Blogs = new HashSet<Blog>();
            this.Dishes = new HashSet<Dish>();
            this.ReservationEmails = new HashSet<string>();
            this.ReservationPhones = new HashSet<string>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [NotMapped]
        public ICollection<string> ReservationEmails { get; set; }

        [NotMapped]
        public ICollection<string> ReservationPhones { get; set; }

        [NotMapped]
        public string Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
