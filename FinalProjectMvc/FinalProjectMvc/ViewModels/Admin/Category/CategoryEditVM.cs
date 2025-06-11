using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.ViewModels.Admin.CategoryType;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required]
        public int CategoryTypeId { get; set; }
        public List<CategoryTypeVM>? CategoryTypes { get; set; }
    }
}
