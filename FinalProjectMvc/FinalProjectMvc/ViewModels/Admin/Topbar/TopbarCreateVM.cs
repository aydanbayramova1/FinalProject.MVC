using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Topbar
{
    public class TopbarCreateVM
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address with '@'.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}
