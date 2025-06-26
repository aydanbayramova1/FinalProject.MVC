using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.IntroVideo
{
    public class IntroVideoCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title can be at most 60 characters long.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required.")]
        [MaxLength(80, ErrorMessage = "Subtitle can be at most 80 characters long.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Subtitle must contain only letters and spaces, no digits or symbols.")]
        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Please upload a background image.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Image must be at most 3MB.")]
        public IFormFile ImgFile { get; set; }

        [Required(ErrorMessage = "Please upload a video file.")]
        [MaxFileSize(10 * 1024 * 1024, ErrorMessage = "Video must be at most 10MB.")]
        public IFormFile VideoFile { get; set; }
    }
}
