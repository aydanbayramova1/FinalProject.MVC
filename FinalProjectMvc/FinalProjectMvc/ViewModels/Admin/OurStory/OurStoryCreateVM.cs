using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OurStory
{
    public class OurStoryCreateVM
    {
        [Required]
        public string SectionTitle { get; set; }

        [Required]
        public string SectionSubtitle { get; set; }

        [Required]
        public string SectionDescription { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
