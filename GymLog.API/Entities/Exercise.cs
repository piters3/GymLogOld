using System.Collections.Generic;

namespace GymLog.API.Entities {
    public class Exercise {

        public Exercise() {
            Workouts = new List<Workout>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int MuscleId { get; set; }
        public int EquipmentId { get; set; }
        //public int? WorkoutId { get; set; }

        public virtual Muscle Muscle { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
