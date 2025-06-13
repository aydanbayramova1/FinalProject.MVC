using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.UI.Account
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
