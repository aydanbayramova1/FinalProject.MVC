using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProjectMvc.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(60, ErrorMessage = "Product name cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Product name cannot contain numbers or special characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product image is required.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Image file size cannot exceed 3MB.")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" }, ErrorMessage = "Only image files (.jpg, .jpeg, .png, .webp) are allowed.")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Ingredients are required.")]
        [MaxLength(100, ErrorMessage = "Ingredients cannot exceed 100 characters.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000, ErrorMessage = "Price must be between 0.01 and 1000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category must be selected.")]
        [Range(1, int.MaxValue, ErrorMessage = "A valid category must be selected.")]
        public int CategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }

        public List<int>? SelectedSizeIds { get; set; }

        public List<SelectListItem>? Sizes { get; set; }

        public Dictionary<int, string> CategoryTypesById { get; set; } = new();
    }
}
