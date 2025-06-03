using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile ImgFile { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
