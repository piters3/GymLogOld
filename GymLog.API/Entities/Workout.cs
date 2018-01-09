using System.Collections.Generic;

namespace GymLog.API.Entities {
    public class Workout {

        public Workout() {
            Daylogs = new List<Daylog>();
        }

        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Rest { get; set; }

        public string UserId { get; set; }
        public int ExerciseId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual ICollection<Daylog> Daylogs { get; set; }
    }
}
