using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class WorkoutViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Serie")]
        public int Sets { get; set; }

        [Required]
        [Display(Name = "Powtórzenia")]
        public int Reps { get; set; }

        [Required]
        [Display(Name = "Obciążenie")]
        public int Weight { get; set; }

        //[Required]
        [Display(Name = "Id użytkownika")]
        public string UserId { get; set; }

        //[Required]
        [Display(Name = "Użytkownik")]
        public virtual UserShortViewModel User { get; set; }

        [Required]
        [Display(Name = "Ćwiczenie")]
        public int ExerciseId { get; set; }
        [Display(Name = "Ćwiczenie")]
        public virtual ExerciseViewModel Exercise { get; set; }
    }
}