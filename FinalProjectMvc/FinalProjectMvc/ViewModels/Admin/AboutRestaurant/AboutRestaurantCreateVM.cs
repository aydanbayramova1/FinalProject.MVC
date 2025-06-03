using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.AboutRestaurant
{
    public class AboutRestaurantCreateVM
    {

       [Required, MaxLength(100)]
       [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
       public string Title { get; set; }

       [Required, MaxLength(100)]
       [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
       public string SubTitle { get; set; }
       [Required, MaxLength(100)]
       public string ExperienceDescription { get; set; }
       [Required, MaxLength(100)]
       public string QualityMessage { get; set; }
       [Required, MaxLength(100)]
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

        [Required, MaxLength(100)]
        public string TimingDescription { get; set; }

        [Required, MaxLength(100)]
        public string WeekdayTime { get; set; }

        [Required, MaxLength(100)]
        public string WeekendTime { get; set; }

        [Required, MaxLength(100)]
        public string HappyHour { get; set; }

        [Required, MaxLength(100)]
        public string HolidayStatus { get; set; }

        [Required, MaxLength(100)]
        public string HolidayNote { get; set; }

        [Required(ErrorMessage = "Please upload a photo.")]
        public IFormFile Photo { get; set; }
       
    }
}

