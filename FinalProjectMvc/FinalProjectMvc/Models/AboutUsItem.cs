namespace FinalProjectMvc.Models
{
    public class AboutUsItem : BaseEntity
    {
        public string IconUrl { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; }
        public int AboutUsId { get; set; }
        public AboutUs AboutUs { get; set; }
    }
}
