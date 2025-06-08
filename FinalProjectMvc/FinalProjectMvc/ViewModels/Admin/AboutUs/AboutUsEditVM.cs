using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Models;

namespace FinalProjectMvc.ViewModels.Admin.AboutUs
{
    public class AboutUsEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? VideoFile { get; set; }
        public string? VideoUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public string OpeningTimeTitle { get; set; }
    }
}
