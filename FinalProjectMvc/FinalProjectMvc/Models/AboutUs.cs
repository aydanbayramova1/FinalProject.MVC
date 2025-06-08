namespace FinalProjectMvc.Models
{
    public class AboutUs : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; } 

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public string ImageUrl { get; set; } 

        public string OpeningTimeTitle { get; set; } 

        public ICollection<OpeningHour> OpeningHours { get; set; }
        public ICollection<AboutUsItem> AboutUsItems { get; set; }
    }
}
