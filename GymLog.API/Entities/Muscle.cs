using System.Collections.Generic;

namespace GymLog.API.Entities {
    public class Muscle {

        public Muscle() {
            Exercises = new List<Exercise>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Exercise> Exercises { get; set; }
    }
}
