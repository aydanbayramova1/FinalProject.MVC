using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.Setting
{
    public class SettingEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "HeaderLogo is required.")]
        public string HeaderLogo { get; set; }

        [Required(ErrorMessage = "FooterLogo is required.")]
        public string FooterLogo { get; set; }

        [Required(ErrorMessage = "BackgroundImage is required.")]
        public string BackgroundImage { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(120, ErrorMessage = "Address can be at most 120 characters long.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^[0-9.,\-]+$", ErrorMessage = "Phone must contain only digits, commas, dots, or hyphens.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Everyday Working Hours is required.")]
        public string EverydayWorkingHours { get; set; }

        [Required(ErrorMessage = "Kitchen Close Time is required.")]
        [RegularExpression(@"^[0-9.,\-]+$", ErrorMessage = "Kitchen Close Time must contain only digits, commas, dots, or hyphens.")]
        public string KitchenCloseTime { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size for HeaderLogoFile is 3MB.")]
        public IFormFile? HeaderLogoFile { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size for FooterLogoFile is 3MB.")]
        public IFormFile? FooterLogoFile { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size for BackgroundImageFile is 3MB.")]
        public IFormFile? BackgroundImageFile { get; set; }
    }
}
