using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
