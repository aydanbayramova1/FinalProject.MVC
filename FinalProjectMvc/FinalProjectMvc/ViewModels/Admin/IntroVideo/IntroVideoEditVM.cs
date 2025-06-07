using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.IntroVideo
{
    public class IntroVideoEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        public string? ExistingImgPath { get; set; } 

        public IFormFile? NewImgFile { get; set; }

        public string? ExistingVideoUrl { get; set; } 

        public IFormFile? NewVideoFile { get; set; } 
    }
}
