using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.AboutUsItem
{
    public class AboutUsItemEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? IconPath { get; set; } 

        public IFormFile? IconFile { get; set; }
    }
}
