using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.MenuBanner
{
    public class MenuBannerCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
