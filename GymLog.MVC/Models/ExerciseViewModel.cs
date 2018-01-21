using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opis ćwiczenia")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Mięśnie użyte podczas ćwiczenia")]
        public IEnumerable<MuscleViewModel> Muscles { get; set; }

        [Required]
        [Display(Name = "Sprzęt")]
        public EquipmentViewModel Equipment { get; set; }
    }
}