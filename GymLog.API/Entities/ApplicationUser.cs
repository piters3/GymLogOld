using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymLog.API.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            Workouts = new List<Workout>();
            Daylogs = new List<Daylog>();
        }

        public virtual ICollection<Daylog> Daylogs { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }


}