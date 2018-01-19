namespace GymLog.MVC.Models
{
    public class UserInfoModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }

        //public IEnumerable<WorkoutModel> Workouts { get; set; }
        //public IEnumerable<DaylogModel> Daylogs { get; set; }
    }
}