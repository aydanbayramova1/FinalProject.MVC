using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.UI.Account
{
   public class LoginVM
   {
        [Required(ErrorMessage = "Username or email cannot be empty.")]
        [MinLength(3, ErrorMessage = "Username or email must be at least 3 characters long.")]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }
}
