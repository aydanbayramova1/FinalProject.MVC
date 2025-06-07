namespace FinalProjectMvc.Models
{
    public class IntroVideo : BaseEntity
    {
        public string Title { get; set; } 
        public string Subtitle { get; set; }
        public string Img { get; set; }
        public string VideoUrl { get; set; } 
        public ICollection<IntroCounter> Counters { get; set; }
    }
}
