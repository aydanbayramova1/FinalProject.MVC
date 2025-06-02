using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Scrolling
{
    public class ScrollingCreateVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Name must contain only letters and spaces, no digits or symbols.")]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
