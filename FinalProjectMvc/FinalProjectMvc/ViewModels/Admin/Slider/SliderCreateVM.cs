using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        [MaxLength(60, ErrorMessage = "Title can be at most 60 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required.")]
        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(90, ErrorMessage = "Description can be at most 90 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 3MB.")]
        public IFormFile Image { get; set; }
    }
}
