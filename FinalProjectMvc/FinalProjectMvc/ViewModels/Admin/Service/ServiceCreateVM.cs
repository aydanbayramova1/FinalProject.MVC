using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Service
{
    public class ServiceCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-zƏəÜüİıÖöÇçĞğŞş\\s]+$", ErrorMessage = "Title must contain only letters and spaces. Digits or symbols are not allowed.")]
        [MaxLength(60, ErrorMessage = "Title can be at most 60 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required.")]
        [RegularExpression("^[A-Za-zƏəÜüİıÖöÇçĞğŞş\\s]+$", ErrorMessage = "Subtitle must contain only letters and spaces. Digits or symbols are not allowed.")]
        [MaxLength(80, ErrorMessage = "Subtitle can be at most 80 characters long.")]
        public string Subtitle { get; set; }
    }
}
