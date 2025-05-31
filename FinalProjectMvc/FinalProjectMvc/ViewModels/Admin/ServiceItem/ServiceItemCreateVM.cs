using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ServiceItem
{
    public class ServiceItemCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}
