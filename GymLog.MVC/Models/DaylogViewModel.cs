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
        public DateTime Date { get; set; }

        //[Required]
        [Display(Name = "Id użytkownika")]
        public string UserId { get; set; }

        //[Required]
        [Display(Name = "Użytkownik")]
        public virtual UserShortViewModel User { get; set; }

        [Display(Name = "Ćwiczenia")]
        public IEnumerable<WorkoutViewModel> Workouts { get; set; }
    }
}