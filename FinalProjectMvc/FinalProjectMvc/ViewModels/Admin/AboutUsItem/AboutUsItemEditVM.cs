using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.AboutUsItem
{
    public class AboutUsItemEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        public string? IconPath { get; set; }

        [MaxFileSize(2 * 1024 * 1024)] 
        public IFormFile? IconFile { get; set; }
    }
}
