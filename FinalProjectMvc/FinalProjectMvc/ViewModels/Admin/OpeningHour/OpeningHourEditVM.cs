using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OpeningHour
{
    public class OpeningHourEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "DayRange is required.")]
        [RegularExpression(@"^[0-9\s:\-]+$", ErrorMessage = "DayRange can only contain digits, spaces, dashes, and colons.")]

        public string DayRange { get; set; }

        [Required(ErrorMessage = "TimeRange is required.")]
        [RegularExpression(@"^[0-9:\s\-]+$", ErrorMessage = "TimeRange can only contain digits, colons, spaces, and dashes.")]
        public string TimeRange { get; set; }
    }
}
