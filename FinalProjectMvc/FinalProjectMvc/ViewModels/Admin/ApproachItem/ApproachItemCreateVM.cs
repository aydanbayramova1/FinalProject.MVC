using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.ApproachItem
{
    public class ApproachItemCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(140, ErrorMessage = "Description cannot exceed 140 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please upload an icon image.")]
        [MaxFileSize(3 * 1024 * 1024)]
        public IFormFile IconFile { get; set; }

        public int ApproachId { get; set; }
    }

}
