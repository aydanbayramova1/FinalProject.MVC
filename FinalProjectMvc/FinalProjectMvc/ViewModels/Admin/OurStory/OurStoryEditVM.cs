using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OurStory
{
    public class OurStoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string SectionTitle { get; set; }

        [Required]
        public string SectionSubtitle { get; set; }

        [Required]
        public string SectionDescription { get; set; }

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; } 
    }
}
