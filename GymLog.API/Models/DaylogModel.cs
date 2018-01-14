using System;
using System.Collections.Generic;

namespace GymLog.API.Models
{
    public class DaylogModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        //public UserModel User { get; set; }
        public IEnumerable<WorkoutModel> Workouts { get; set; }
    }
}