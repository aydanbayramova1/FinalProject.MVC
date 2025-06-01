using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.TeamBanner
{
    public class TeamBannerCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
