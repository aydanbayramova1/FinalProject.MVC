using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.FaqCategory
{
    public class FaqCategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
    }
}

