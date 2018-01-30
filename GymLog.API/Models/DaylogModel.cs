using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymLog.API.Models
{
    public class DaylogModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual UserShortViewModel User { get; set; }
        public IEnumerable<WorkoutModel> Workouts { get; set; }
    }
}