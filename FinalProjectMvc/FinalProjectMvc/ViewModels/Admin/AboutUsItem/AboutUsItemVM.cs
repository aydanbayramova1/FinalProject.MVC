using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.AboutUsItem
{
    public class AboutUsItemVM
    {
        public int? Id { get; set; }

        public IFormFile? IconFile { get; set; } 
        public string? IconUrl { get; set; }    

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
