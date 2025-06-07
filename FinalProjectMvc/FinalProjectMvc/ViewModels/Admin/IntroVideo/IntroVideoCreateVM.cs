using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.IntroVideo
{
    public class IntroVideoCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Please upload a background image.")]
        public IFormFile ImgFile { get; set; } 

        [Required(ErrorMessage = "Please upload a video file.")]
        public IFormFile VideoFile { get; set; }
    }
}
