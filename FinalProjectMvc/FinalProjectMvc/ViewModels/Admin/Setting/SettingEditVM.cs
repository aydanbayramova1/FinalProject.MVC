using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Setting
{
    public class SettingEditVM
    {
        public int Id { get; set; }
        public string HeaderLogo { get; set; }
        public string FooterLogo { get; set; }
        public string BackgroundImage { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string EverydayWorkingHours { get; set; }
        public string KitchenCloseTime { get; set; }
        public IFormFile? HeaderLogoFile { get; set; }
        public IFormFile? FooterLogoFile { get; set; }
        public IFormFile? BackgroundImageFile { get; set; }
    }
}
