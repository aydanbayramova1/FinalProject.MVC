using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.ApproachItem
{
    public class ApproachItemEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(90, ErrorMessage = "Description cannot exceed 90 characters.")]
        public string Description { get; set; }

        public string? IconImg { get; set; }

        [MaxFileSize(3 * 1024 * 1024)] 
        public IFormFile? IconFile { get; set; }

        public int ApproachId { get; set; }
    }
}
