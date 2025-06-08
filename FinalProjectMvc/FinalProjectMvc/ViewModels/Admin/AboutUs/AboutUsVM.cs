using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;

namespace FinalProjectMvc.ViewModels.Admin.AboutUs
{
    public class AboutUsVM
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Description { get; set; }

        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public string OpeningTimeTitle { get; set; }
        public List<OpeningHourVM> OpeningHours { get; set; } = new();
        public List<AboutUsItemVM> AboutUsItems { get; set; } = new();
    }
}
