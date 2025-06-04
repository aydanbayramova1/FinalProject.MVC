using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.AboutRestaurant
{
    public class AboutRestaurantCreateVM
    {

       [Required]
       [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
       public string Title { get; set; }

       [Required]
       [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
       public string SubTitle { get; set; }
       [Required]
       public string ExperienceDescription { get; set; }
       [Required]
       public string QualityMessage { get; set; }
       [Required]
       public string AtmosphereNote { get; set; }

       [Required]
       [RegularExpression(@"^[a-zA-Z\s\-]+$", ErrorMessage = "Only letters allowed.")]
        public string HeadBaristaName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
         public string TimingTitle { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
        public string TimingSubtitle { get; set; }

        [Required]
        public string TimingDescription { get; set; }

        [Required]
        public string WeekdayTime { get; set; }

        [Required]
        public string WeekendTime { get; set; }

        [Required]
        public string HappyHour { get; set; }

        [Required]
        public string HolidayStatus { get; set; }

        [Required]
        public string HolidayNote { get; set; }

        [Required(ErrorMessage = "Please upload a photo.")]
        public IFormFile Photo { get; set; }
       
    }
}

