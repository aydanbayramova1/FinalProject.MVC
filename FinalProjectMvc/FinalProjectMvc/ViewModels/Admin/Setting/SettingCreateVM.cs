using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Setting
{
    public class SettingCreateVM
    {
        [Required]
        public IFormFile HeaderLogo { get; set; }

        [Required]
        public IFormFile FooterLogo { get; set; }

        [Required]
        public IFormFile BackgroundImage { get; set; }

        [Required]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string EverydayWorkingHours { get; set; }

        [Required]
        public string KitchenCloseTime { get; set; }
    }
}
