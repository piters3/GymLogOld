using GymLog.API.Entities;
using System.Linq;

namespace GymLog.API.Models
{
    public class ModelFactory
    {
        public MuscleModel Create(Muscle muscle)
        {
            return new MuscleModel()
            {
                Id = muscle.Id,
                Name = muscle.Name
            };
        }


        public EquipmentModel Create(Equipment eq)
        {
            return new EquipmentModel()
            {
                Id = eq.Id,
                Name = eq.Name
            };
        }


        public ExerciseModel Create(Exercise ex)
        {
            return new ExerciseModel()
            {
                Id = ex.Id,
                Name = ex.Name,
                Description = ex.Description,
                Muscles = ex.Muscles.Select(m => Create(m)),
                Equipment = Create(ex.Equipment)
            };
        }


        public UserModel Create(User u)
        {
            return new UserModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                PhoneNumber = u.PhoneNumber,
                AccessFailedCount = u.AccessFailedCount,
                Claims = u.Claims,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEndDateUtc = u.LockoutEndDateUtc,
                Logins = u.Logins,
                PasswordHash = u.PasswordHash,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                Roles = u.Roles,
                SecurityStamp = u.SecurityStamp,
                TwoFactorEnabled = u.TwoFactorEnabled,
                Workouts = u.Workouts.Select(w => Create(w)),
                Daylogs = u.Daylogs.Select(d => Create(d))
            };
        }


        public WorkoutModel Create(Workout w)
        {
            return new WorkoutModel()
            {
                Id = w.Id,
                Reps = w.Reps,
                Sets = w.Sets,
                Rest = w.Rest,
                UserId = w.UserId,
                //User = Create(w.User), //Odrzuca połączenie
                Exercise = Create(w.Exercise)
            };
        }


        public DaylogModel Create(Daylog d)
        {
            return new DaylogModel()
            {
                Id = d.Id,
                Date = d.Date,
                //User = Create(d.User),    //Odrzuca połączenie
                Workouts = d.Workouts.Select(w => Create(w)),
                UserId = d.UserId
            };
        }
    }
}