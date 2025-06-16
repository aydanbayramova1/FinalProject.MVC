using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.Catalog
{
    public class CatalogCreateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(90, ErrorMessage = "Description cannot exceed 90 characters.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Description must contain only letters and spaces.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [MaxFileSize(3 * 1024 * 1024)] 
        public IFormFile Image { get; set; }
    }
}
