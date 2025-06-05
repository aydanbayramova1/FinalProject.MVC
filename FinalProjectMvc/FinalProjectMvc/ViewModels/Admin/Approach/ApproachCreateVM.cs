using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Approach
{
    public class ApproachCreateVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters.")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Subtitle can only contain letters.")]
        public string SubTitle { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
