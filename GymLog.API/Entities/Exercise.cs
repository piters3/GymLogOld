using System.Collections.Generic;

namespace GymLog.API.Entities {
    public class Exercise {

        public Exercise() {
            Workouts = new List<Workout>();
            Muscles = new List<Muscle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int EquipmentId { get; set; }

        public virtual List<Muscle> Muscles { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual List<Workout> Workouts { get; set; }
    }
}
