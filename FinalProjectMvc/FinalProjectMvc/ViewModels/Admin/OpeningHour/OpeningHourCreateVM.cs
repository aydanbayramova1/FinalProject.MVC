using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OpeningHour
{
    public class OpeningHourCreateVM
    {
        [Required]
        public string DayRange { get; set; }

        [Required]
        public string TimeRange { get; set; }

    }
}
