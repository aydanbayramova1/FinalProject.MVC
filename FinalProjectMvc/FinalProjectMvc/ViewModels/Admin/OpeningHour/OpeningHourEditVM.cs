using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OpeningHour
{
    public class OpeningHourEditVM
    {
        public int Id { get; set; }

        [Required]
        public string DayRange { get; set; }

        [Required]
        public string TimeRange { get; set; }

    }
}
