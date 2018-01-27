using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opis ćwiczenia")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Ćwiczony mięsień")]
        public int MuscleId { get; set; }
        [Display(Name = "Ćwiczony mięsień")]
        public virtual MuscleViewModel Muscle { get; set; }

        [Required]
        [Display(Name = "Sprzęt")]
        public int EquipmentId { get; set; }
        [Display(Name = "Sprzęt")]
        public virtual EquipmentViewModel Equipment { get; set; }
    }
}