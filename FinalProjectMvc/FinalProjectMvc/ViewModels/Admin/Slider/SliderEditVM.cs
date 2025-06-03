using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Img { get; set; } 
        public IFormFile? Photo { get; set; }
    }    
}
