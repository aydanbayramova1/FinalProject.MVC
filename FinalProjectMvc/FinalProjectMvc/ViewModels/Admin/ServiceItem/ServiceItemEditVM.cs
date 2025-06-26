using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.ServiceItem
{
    public class ServiceItemEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        [MaxLength(60, ErrorMessage = "Title can be at most 60 characters long.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Subtitle is required.")]
        [MaxLength(150, ErrorMessage = "Subtitle can be at most 150 characters long.")]
        [RegularExpression(@"^[A-Za-zƏəÜüİıÖöÇçĞğŞş\s\.,!\?:'\-–—]+$", ErrorMessage = "Subtitle can contain letters, punctuation and spaces, but no digits.")]

        public string Subtitle { get; set; }


        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 3MB.")]
        public IFormFile? Photo { get; set; }

        public string? Img { get; set; }
    }
}
