using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.FaqsBanner
{
    public class FaqsBannerCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
