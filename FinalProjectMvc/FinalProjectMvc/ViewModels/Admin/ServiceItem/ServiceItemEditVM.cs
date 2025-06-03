using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ServiceItem
{
    public class ServiceItemEditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Img { get; set; }
    }
}
