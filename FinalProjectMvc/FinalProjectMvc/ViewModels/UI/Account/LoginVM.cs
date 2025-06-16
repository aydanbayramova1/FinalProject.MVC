using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.UI.Account
{
   public class LoginVM
   {
        [Required(ErrorMessage = "Username or email cannot be empty.")]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       public bool IsRememberMe { get; set; }
    }
}
