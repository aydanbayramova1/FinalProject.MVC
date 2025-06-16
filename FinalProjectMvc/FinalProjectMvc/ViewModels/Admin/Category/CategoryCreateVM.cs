using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;
using FinalProjectMvc.ViewModels.Admin.CategoryType;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.Category
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(60, ErrorMessage = "Category name cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can contain only letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please upload an image.")]
        [MaxFileSize(3 * 1024 * 1024)] 
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Please select a Category Type.")]
        public int? CategoryTypeId { get; set; }

        public List<CategoryTypeVM>? CategoryTypes { get; set; }
    }
}
