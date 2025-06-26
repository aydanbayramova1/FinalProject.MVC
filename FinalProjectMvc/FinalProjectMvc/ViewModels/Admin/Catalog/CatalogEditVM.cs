using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.Catalog
{
    public class CatalogEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(90, ErrorMessage = "Description cannot exceed 90 characters.")]
        public string Description { get; set; }


        public string? Img { get; set; }

        [MaxFileSize(3 * 1024 * 1024)] 
        public IFormFile? Photo { get; set; }
    }
}
