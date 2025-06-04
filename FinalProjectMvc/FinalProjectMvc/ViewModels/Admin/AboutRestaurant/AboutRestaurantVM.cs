using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.AboutRestaurant
{
    public class AboutRestaurantVM
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
        public string SubTitle { get; set; }

        public string ExperienceDescription { get; set; }
        public string QualityMessage { get; set; }
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

        public string TimingDescription { get; set; }
        public string WeekdayTime { get; set; }
        public string WeekendTime { get; set; }
        public string HappyHour { get; set; }
        public string HolidayStatus { get; set; }
        public string HolidayNote { get; set; }

        public string? Image { get; set; }
    }

}
