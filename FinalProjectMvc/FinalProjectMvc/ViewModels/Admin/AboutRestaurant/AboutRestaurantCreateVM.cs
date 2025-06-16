using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.AboutRestaurant
{
    public class AboutRestaurantCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters and spaces are allowed in Title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "SubTitle is required.")]
        [MaxLength(60, ErrorMessage = "SubTitle cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters and spaces are allowed in SubTitle.")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Experience Description is required.")]
        public string ExperienceDescription { get; set; }

        [Required(ErrorMessage = "Quality Message is required.")]
        public string QualityMessage { get; set; }

        [Required(ErrorMessage = "Atmosphere Note is required.")]
        public string AtmosphereNote { get; set; }

        [Required(ErrorMessage = "Head Barista Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s\-]+$", ErrorMessage = "Only letters, spaces and hyphens are allowed in Head Barista Name.")]
        public string HeadBaristaName { get; set; }

        [Required(ErrorMessage = "Timing Title is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters and spaces are allowed in Timing Title.")]
        public string TimingTitle { get; set; }

        [Required(ErrorMessage = "Timing Subtitle is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters and spaces are allowed in Timing Subtitle.")]
        public string TimingSubtitle { get; set; }

        [Required(ErrorMessage = "Timing Description is required.")]
        public string TimingDescription { get; set; }

        [Required(ErrorMessage = "Weekday Time is required.")]
        public string WeekdayTime { get; set; }

        [Required(ErrorMessage = "Weekend Time is required.")]
        public string WeekendTime { get; set; }

        [Required(ErrorMessage = "Happy Hour is required.")]
        public string HappyHour { get; set; }

        [Required(ErrorMessage = "Holiday Status is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Holiday Status must be a positive number or zero.")]
        public string HolidayStatus { get; set; }

        [Required(ErrorMessage = "Holiday Note is required.")]
        public string HolidayNote { get; set; }

        [Required(ErrorMessage = "Please upload a photo.")]
        [MaxFileSize(2 * 1024 * 1024)] 
        public IFormFile Photo { get; set; }
    }
}