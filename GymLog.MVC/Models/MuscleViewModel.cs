using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class MuscleViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
    }
}