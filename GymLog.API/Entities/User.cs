using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymLog.API.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Workouts = new List<Workout>();
            Daylogs = new List<Daylog>();
        }

        public virtual List<Daylog> Daylogs { get; set; }
        public virtual List<Workout> Workouts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }


}