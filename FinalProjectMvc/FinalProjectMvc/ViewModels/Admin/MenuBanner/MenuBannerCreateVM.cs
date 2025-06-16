using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.MenuBanner
{
    public class MenuBannerCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(60, ErrorMessage = "Title must be max 60 characters.")]
        [RegularExpression(@"^(?!\s*$)[A-Za-z\s]+$", ErrorMessage = "Title must contain only letters and spaces, not just spaces or digits.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Photo is required.")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".webp" })]
        [MaxFileSize(2 * 1024 * 1024)]
        public IFormFile Photo { get; set; }
    }
}
