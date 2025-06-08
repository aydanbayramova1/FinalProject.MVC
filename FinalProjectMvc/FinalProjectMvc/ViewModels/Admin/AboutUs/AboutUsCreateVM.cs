using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Models;

namespace FinalProjectMvc.ViewModels.Admin.AboutUs
{
    public class AboutUsCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile VideoFile { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string OpeningTimeTitle { get; set; }
    }
}
