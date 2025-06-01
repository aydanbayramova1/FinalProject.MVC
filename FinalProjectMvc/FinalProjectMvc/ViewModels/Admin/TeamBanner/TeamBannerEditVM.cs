using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.TeamBanner
{
    public class TeamBannerEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Img { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
