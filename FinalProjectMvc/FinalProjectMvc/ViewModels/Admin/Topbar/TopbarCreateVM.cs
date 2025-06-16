using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Topbar
{
    public class TopbarCreateVM
    {
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(70, ErrorMessage = "Email can be at most 70 characters.")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
            ErrorMessage = "Please enter a valid email address with exactly one '@' symbol.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(120, ErrorMessage = "Address can be at most 120 characters.")]
        public string Address { get; set; }
    }
}
