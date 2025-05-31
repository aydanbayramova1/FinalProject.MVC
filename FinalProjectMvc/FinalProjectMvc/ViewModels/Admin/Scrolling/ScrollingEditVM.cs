using Microsoft.Build.Framework;

namespace FinalProjectMvc.ViewModels.Admin.Scrolling
{
    public class ScrollingEditVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Img { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
