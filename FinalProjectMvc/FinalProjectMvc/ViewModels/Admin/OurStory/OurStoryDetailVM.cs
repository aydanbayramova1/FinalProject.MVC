using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.StoryItem;

namespace FinalProjectMvc.ViewModels.Admin.OurStory
{
    public class OurStoryDetailVM
    {
        public int Id { get; set; }
        public string SectionTitle { get; set; }
        public string SectionSubtitle { get; set; }
        public string SectionDescription { get; set; }
        public string Image { get; set; }
        public List<StoryItemVM> StoryItems { get; set; }
    }

}
