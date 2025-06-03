using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? ImgFile { get; set; } 
        public string? Img { get; set; } 
    }
}
