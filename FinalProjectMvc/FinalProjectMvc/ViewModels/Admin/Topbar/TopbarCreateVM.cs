using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Topbar
{
    public class TopbarCreateVM
    {
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(50, ErrorMessage = "Email can be at most 50 characters.")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
              ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(100, ErrorMessage = "Address can be at most 100 characters.")]
        public string Address { get; set; }
    }
}
