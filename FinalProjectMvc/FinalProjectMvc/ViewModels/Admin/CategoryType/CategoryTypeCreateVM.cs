using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.CategoryType
{
    public class CategoryTypeCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
