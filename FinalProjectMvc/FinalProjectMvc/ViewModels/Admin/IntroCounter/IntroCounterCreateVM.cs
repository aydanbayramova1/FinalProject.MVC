using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.IntroCounter
{
    public class IntroCounterCreateVM
    {
        [Required(ErrorMessage = "Icon seçilməlidir")]
        public IFormFile IconFile { get; set; }

        [Required(ErrorMessage = "Say göstərilməlidir")]
        [Range(0, int.MaxValue, ErrorMessage = "Say 0 və ya daha böyük olmalıdır")]
        public int Count { get; set; }

        public string? Suffix { get; set; }

        [Required(ErrorMessage = "Təsvir boş ola bilməz")]
        public string Description { get; set; }

    }
}
