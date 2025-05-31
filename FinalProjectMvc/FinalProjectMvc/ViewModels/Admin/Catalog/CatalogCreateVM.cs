using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Catalog
{
    public class CatalogCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
