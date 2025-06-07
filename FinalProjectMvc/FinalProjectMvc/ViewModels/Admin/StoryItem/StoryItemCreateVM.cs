using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.StoryItem
{
    public class StoryItemCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile IconFile { get; set; }

        [Required]
        public int OurStoryId { get; set; }
    }
}
