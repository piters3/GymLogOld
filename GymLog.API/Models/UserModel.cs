using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace GymLog.API.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public int AccessFailedCount { get; set; }
        public ICollection<IdentityUserClaim> Claims { get; set; }      
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public ICollection<IdentityUserLogin> Logins { get; set; }
        public string PasswordHash { get; set; }        
        public bool PhoneNumberConfirmed { get; set; }
        public ICollection<IdentityUserRole> Roles { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public IEnumerable<WorkoutModel> Workouts { get; set; }
        public IEnumerable<DaylogModel> Daylogs { get; set; }
    }
}