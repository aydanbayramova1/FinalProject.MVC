using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ContactBanner
{
    public class ContactBannerEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Img { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
