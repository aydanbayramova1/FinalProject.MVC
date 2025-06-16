using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.TeamBanner
{
    public class TeamBannerEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(60, ErrorMessage = "Title must be at most 60 characters.")]
        [RegularExpression(@"^(?!\s*$)[A-Za-z\s]+$", ErrorMessage = "Title must contain only letters and spaces. Digits, symbols, and negative numbers are not allowed.")]
        public string Title { get; set; }

        public string Img { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" })]
        [MaxFileSize(2 * 1024 * 1024)]
        public IFormFile? Photo { get; set; }
    }
}
