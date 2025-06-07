using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.UI
{
    public class LoginVM
    {
        [Required(ErrorMessage = "İstifadəçi adı və ya email tələb olunur")]
        [Display(Name = "İstifadəçi adı və ya Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Şifrə tələb olunur")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [Display(Name = "Yadda saxla")]
        public bool RememberMe { get; set; }
    }
}
