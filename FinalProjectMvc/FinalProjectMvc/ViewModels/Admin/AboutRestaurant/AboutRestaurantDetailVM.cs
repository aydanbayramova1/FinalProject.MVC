using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.AboutRestaurant
{
    public class AboutRestaurantDetailVM
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
        public string Image { get; set; }


    }
}
