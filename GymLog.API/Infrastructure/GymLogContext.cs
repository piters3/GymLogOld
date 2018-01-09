using GymLog.API.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GymLog.API.Infrastructure
{
    public class GymLogContext : IdentityDbContext<ApplicationUser>
    {
        public GymLogContext() : base("GymlogDB", throwIfV1Schema: false)
        {
        }

        public DbSet<Daylog> Daylogs { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public static GymLogContext Create()
        {
            return new GymLogContext();
        }
    }
}