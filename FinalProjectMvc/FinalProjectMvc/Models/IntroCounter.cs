namespace FinalProjectMvc.Models
{
    public class IntroCounter :BaseEntity
    {
        public string IconPath { get; set; }
        public int Count { get; set; } 
        public string Suffix { get; set; } = "+";
        public string Description { get; set; } 
        public int IntroVideoId { get; set; }
        public IntroVideo IntroVideo { get; set; }
    }
}
