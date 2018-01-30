using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class DaylogViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data planu")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //[Required]
        [Display(Name = "Id użytkownika")]
        public string UserId { get; set; }

        //public int[] ExercisesIds { get; set; }


        //[Required]
        [Display(Name = "Użytkownik")]
        public virtual UserShortViewModel User { get; set; }

        public int[] WorkoutsIds { get; set; }
        public int[] ExerciseDetails { get; set; }

        //[Required]
        [Display(Name = "Ćwiczenia")]
        public IEnumerable<WorkoutViewModel> Workouts { get; set; }
    }
}