using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Size
{
    public class SizeCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
