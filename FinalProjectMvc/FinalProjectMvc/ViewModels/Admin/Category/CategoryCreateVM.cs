using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.ViewModels.Admin.CategoryType;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Please select a Category Type")]
        public int? CategoryTypeId { get; set; }

        public List<CategoryTypeVM>? CategoryTypes { get; set; }
    }
}
