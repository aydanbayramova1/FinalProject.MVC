using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.Scrolling
{
    public class ScrollingEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-zƏəÜüİıÖöÇçĞğŞş\\s]+$", ErrorMessage = "Title must contain only letters and spaces. Digits or symbols are not allowed.")]
        [MaxLength(60, ErrorMessage = "Title can be at most 60 characters long.")]
        public string Name { get; set; }

        public string? Img { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Image must be less than or equal to 3 MB.")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" }, ErrorMessage = "Only .jpg, .jpeg, .png, or .webp formats are allowed.")]
        public IFormFile? Photo { get; set; }
    }
}
