using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Slider
{
    public class SliderEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Img { get; set; } 
        public IFormFile? Photo { get; set; }
    }    
}
