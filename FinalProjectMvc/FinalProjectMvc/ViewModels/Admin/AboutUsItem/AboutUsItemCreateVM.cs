using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Models;

namespace FinalProjectMvc.ViewModels.Admin.AboutUsItem
{
    public class AboutUsItemCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile IconFile { get; set; }

    }
}
