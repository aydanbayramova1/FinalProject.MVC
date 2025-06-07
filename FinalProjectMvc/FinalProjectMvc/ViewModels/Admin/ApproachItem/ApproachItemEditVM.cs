using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ApproachItem
{
    public class ApproachItemEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? IconImg { get; set; }

        public IFormFile? IconFile { get; set; }

        public int ApproachId { get; set; }
    }
}
