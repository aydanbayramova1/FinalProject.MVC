using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;
using FinalProjectMvc.ViewModels.Admin.CategoryType;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(60, ErrorMessage = "Category name cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can contain only letters and spaces.")]
        public string Name { get; set; }

        public string? ExistingImage { get; set; }

        [MaxFileSize(3 * 1024 * 1024)]  
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Please select a Category Type.")]
        public int CategoryTypeId { get; set; }

        public List<CategoryTypeVM>? CategoryTypes { get; set; }
    }
}
