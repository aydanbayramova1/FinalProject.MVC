namespace FinalProjectMvc.Models
{
    public class Approach : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }
        public ICollection<ApproachItem> Items { get; set; }
    }
}
