using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.IntroVideo
{
    public class IntroVideoEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title can be at most 60 characters long.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required.")]
        [MaxLength(80, ErrorMessage = "Subtitle can be at most 80 characters long.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Subtitle must contain only letters and spaces, no digits or symbols.")]
        public string Subtitle { get; set; }

        public string? ExistingImgPath { get; set; }

        [MaxFileSize(3)] 
        public IFormFile? NewImgFile { get; set; }

        public string? ExistingVideoUrl { get; set; }

        [MaxFileSize(50)] 
        public IFormFile? NewVideoFile { get; set; }
    }
}
