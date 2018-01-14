namespace GymLog.API.Models
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Rest { get; set; }

        public string UserId { get; set; }

        //public virtual UserModel User { get; set; }
        public virtual ExerciseModel Exercise { get; set; }
    }
}