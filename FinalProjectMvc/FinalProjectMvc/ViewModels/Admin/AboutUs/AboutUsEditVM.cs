using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;
using FinalProjectMvc.Models;

namespace FinalProjectMvc.ViewModels.Admin.AboutUs
{
    public class AboutUsEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can contain only letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required.")]
        [MaxLength(60, ErrorMessage = "Subtitle cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Subtitle can contain only letters and spaces.")]
        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? VideoFile { get; set; }

        public string? VideoUrl { get; set; }

        [MaxFileSize(5 * 1024 * 1024)] 
        public IFormFile? ImageFile { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Opening Time Title is required.")]
        [MaxLength(60, ErrorMessage = "Opening Time Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Opening Time Title can contain only letters and spaces.")]
        public string OpeningTimeTitle { get; set; }
    }
}
