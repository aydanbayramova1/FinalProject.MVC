using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.UI.Account
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address with '@' symbol.")]
        public string Email { get; set; }
    }
}
