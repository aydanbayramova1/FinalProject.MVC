using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Service
{
    public class ServiceCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
    }
}
