using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ServiceItem
{
    public class ServiceItemCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}
