using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;

namespace FinalProjectMvc.ViewModels.Admin.AboutUs
{
    public class AboutUsDetailVM
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string OpeningTimeTitle { get; set; }
        public List<OpeningHourVM> OpeningHours { get; set; }
        public List<AboutUsItemDetailVM> AboutUsItems { get; set; }
    }
}
