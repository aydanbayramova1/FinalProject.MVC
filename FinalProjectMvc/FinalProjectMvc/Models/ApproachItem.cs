namespace FinalProjectMvc.Models
{
    public class ApproachItem :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int ApproachId { get; set; }
        public Approach Approach { get; set; }
    }
}
