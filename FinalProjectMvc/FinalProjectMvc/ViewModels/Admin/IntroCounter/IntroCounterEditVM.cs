using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.IntroCounter
{
    public class IntroCounterEditVM
    {
        public int Id { get; set; }

        public string? ExistingIconPath { get; set; }

        [Required(ErrorMessage = "Icon must be selected")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 3MB.")]
        public IFormFile? NewIconFile { get; set; }

        [Required(ErrorMessage = "Count is required")]
        [Range(0, 2000, ErrorMessage = "Count must be between 0 and 2000")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Count must contain only digits")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Suffix is required.")]
        [RegularExpression(@"^\+$", ErrorMessage = "Suffix can only be the '+' character.")]
        public string Suffix { get; set; } = "+";

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(50, ErrorMessage = "Description can be at most 50 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IntroVideoId is required.")]
        public int IntroVideoId { get; set; }
    }
}
