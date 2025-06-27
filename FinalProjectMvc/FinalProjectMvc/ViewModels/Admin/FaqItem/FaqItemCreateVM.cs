using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.FaqItem
{
    public class FaqItemCreateVM
    {
        [Required(ErrorMessage = "Question is required")]
        [MinLength(1, ErrorMessage = "Question cannot be empty")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        [MinLength(1, ErrorMessage = "Answer cannot be empty")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Category selection is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        [Display(Name = "FAQ Category")]
        public int FaqCategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }
    }
}
