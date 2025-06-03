namespace FinalProjectMvc.Models
{
    public class AboutRestaurant :  BaseEntity
    {
        public string Title { get; set; }            
        public string SubTitle { get; set; }
        public string ExperienceDescription { get; set; }  
        public string QualityMessage { get; set; }          
        public string AtmosphereNote { get; set; }            
        public string HeadBaristaName { get; set; }
        public string TimingTitle { get; set; }        
        public string TimingSubtitle { get; set; }    
        public string TimingDescription { get; set; }  
        public string WeekdayTime { get; set; }       
        public string WeekendTime { get; set; }        
        public string HappyHour { get; set; }     
        public string HolidayStatus { get; set; }  
        public string HolidayNote { get; set; }
        public string Image { get; set; }
    }
}
