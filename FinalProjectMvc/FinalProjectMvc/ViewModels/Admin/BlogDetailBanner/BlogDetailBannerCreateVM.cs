using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.BlogDetailBanner
{
    public class BlogDetailBannerCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
