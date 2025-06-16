using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OpeningHour
{
    public class OpeningHourCreateVM
    {
        [Required(ErrorMessage = "DayRange is required.")]
        [RegularExpression(@"^[0-9\s\-]+$", ErrorMessage = "DayRange can only contain digits, spaces and dashes.")]
        public string DayRange { get; set; }

        [Required(ErrorMessage = "TimeRange is required.")]
        [RegularExpression(@"^[0-9:\s\-]+$", ErrorMessage = "TimeRange can only contain digits, colon, spaces and dashes.")]
        public string TimeRange { get; set; }
    }
}
