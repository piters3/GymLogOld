using System.ComponentModel.DataAnnotations;

namespace GymLog.MVC.Models
{
    public class UserShortViewModel
    {
        [Required]
        [Display(Name = "Id użytkownika")]
        public string Id { get; set; }

        [Display(Name = "Użytkownik")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}