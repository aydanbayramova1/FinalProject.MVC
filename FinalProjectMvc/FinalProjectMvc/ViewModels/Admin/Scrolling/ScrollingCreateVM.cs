using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.Scrolling
{
    public class ScrollingCreateVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[A-Za-zƏəÜüİıÖöÇçĞğŞş\\s]+$", ErrorMessage = "Name must contain only letters and spaces. Digits and symbols are not allowed.")]
        [MaxLength(60, ErrorMessage = "Name can be at most 60 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [MaxFileSize(3 * 1024 * 1024)] 
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".webp" })]
        public IFormFile Image { get; set; }
    }
}
