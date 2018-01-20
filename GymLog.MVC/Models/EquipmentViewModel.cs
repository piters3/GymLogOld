using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
    }
}