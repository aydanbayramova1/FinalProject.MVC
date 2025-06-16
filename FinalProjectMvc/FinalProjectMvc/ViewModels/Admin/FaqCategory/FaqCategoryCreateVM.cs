using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.FaqCategory
{
    public class FaqCategoryCreateVM
    {
        [Required(ErrorMessage = "Category name is required.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Category name must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }
    }
}

