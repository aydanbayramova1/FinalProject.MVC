using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.StoryItem
{
    public class StoryItemEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? IconImg { get; set; }

        public IFormFile? IconFile { get; set; }

        public int OurStoryId { get; set; }
    }
}
