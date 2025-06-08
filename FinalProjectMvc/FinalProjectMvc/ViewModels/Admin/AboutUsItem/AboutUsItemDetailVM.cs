using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;

namespace FinalProjectMvc.ViewModels.Admin.AboutUsItem
{
    public class AboutUsItemDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string IconPath { get; set; } 
    }
}
