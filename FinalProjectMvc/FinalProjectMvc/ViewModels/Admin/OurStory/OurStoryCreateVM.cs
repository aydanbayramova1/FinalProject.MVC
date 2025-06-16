using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.OurStory
{
    public class OurStoryCreateVM
    {
        [Required(ErrorMessage = "Section title is required.")]
        [MaxLength(60, ErrorMessage = "Section title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Section title cannot contain numbers or special characters.")]
        public string SectionTitle { get; set; }

        [Required(ErrorMessage = "Section subtitle is required.")]
        [MaxLength(70, ErrorMessage = "Section subtitle cannot exceed 70 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Section subtitle cannot contain numbers or special characters.")]
        public string SectionSubtitle { get; set; }

        [Required(ErrorMessage = "Section description is required.")]
        [MaxLength(90, ErrorMessage = "Section description cannot exceed 90 characters.")]
        public string SectionDescription { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Image file size cannot exceed 3MB.")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" }, ErrorMessage = "Only image files (.jpg, .jpeg, .png, .webp) are allowed.")]
        public IFormFile ImageFile { get; set; }
    }
}
