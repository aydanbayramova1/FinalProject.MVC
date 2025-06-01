using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.BlogBanner
{
    public class BlogBannerEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Img { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
