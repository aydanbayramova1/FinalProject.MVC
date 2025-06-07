namespace FinalProjectMvc.Models
{
    public class StoryItem : BaseEntity
    {
        public string IconImg { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OurStoryId { get; set; }
        public OurStory OurStory { get; set; }
    }
}
