namespace GymLog.API.Models
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }

        //public string UserId { get; set; }

        public virtual UserShortViewModel User { get; set; }
        public virtual ExerciseModel Exercise { get; set; }
    }
}