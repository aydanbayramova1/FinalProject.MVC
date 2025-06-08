namespace FinalProjectMvc.Models
{
    public class OpeningHour : BaseEntity
    {
        public string DayRange { get; set; } 
        public string TimeRange { get; set; } 

        public int AboutUsId { get; set; }
        public AboutUs AboutUs { get; set; }
    }
}
