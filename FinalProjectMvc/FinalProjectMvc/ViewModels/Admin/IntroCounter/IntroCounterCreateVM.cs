using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.IntroCounter
{
    public class IntroCounterCreateVM
    {
        [Required(ErrorMessage = "Icon must be selected")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 3MB.")]
        public IFormFile IconFile { get; set; }

        [Required(ErrorMessage = "Count is required")]
        [Range(0, 2000, ErrorMessage = "Count must be between 0 and 2000")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Count must contain only digits")]
        public int Count { get; set; }
        [PlusOnly]
        public string? Suffix { get; set; }

        [Required(ErrorMessage = "Description cannot be empty")]
        [MaxLength(50, ErrorMessage = "Description can be at most 50 characters long")]
        public string Description { get; set; }

    }
}
