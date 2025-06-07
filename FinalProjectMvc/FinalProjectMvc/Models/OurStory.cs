namespace FinalProjectMvc.Models
{
    public class OurStory : BaseEntity
    {
        public string SectionTitle { get; set; }
        public string SectionSubtitle { get; set; }
        public string SectionDescription { get; set; }
        public string Image { get; set; }
        public ICollection<StoryItem> StoryItems { get; set; }
    }
}
