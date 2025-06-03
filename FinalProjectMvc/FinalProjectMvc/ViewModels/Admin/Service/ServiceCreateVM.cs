using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Service
{
    public class ServiceCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Subtitle is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Subtitle must contain only letters and spaces, no digits or symbols.")]
        public string Subtitle { get; set; }
    }
}
