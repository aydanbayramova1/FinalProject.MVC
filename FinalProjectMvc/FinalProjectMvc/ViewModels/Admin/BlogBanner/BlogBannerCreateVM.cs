using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.BlogBanner
{
    public class BlogBannerCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
