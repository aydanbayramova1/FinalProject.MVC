using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Scrolling
{
    public class ScrollingCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
