using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Catalog
{
    public class CatalogEditVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Img { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
