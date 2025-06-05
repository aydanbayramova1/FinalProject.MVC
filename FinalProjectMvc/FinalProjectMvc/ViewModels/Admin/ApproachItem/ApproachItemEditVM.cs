using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ApproachItem
{
    public class ApproachItemEditVM
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title can only contain letters.")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string IconPath { get; set; }

        public IFormFile? IconFile { get; set; }

        public int ApproachId { get; set; }
    }
}
