using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ContactBanner
{
    public class ContactBannerCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
