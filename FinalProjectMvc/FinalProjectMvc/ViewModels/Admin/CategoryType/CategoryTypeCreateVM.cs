using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.CategoryType
{
    public class CategoryTypeCreateVM
    {
        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(60, ErrorMessage = "Category name cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can contain only letters and spaces.")]
        public string Name { get; set; }

    }
}
