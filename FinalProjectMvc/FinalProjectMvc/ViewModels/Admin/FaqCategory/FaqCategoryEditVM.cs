using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.FaqCategory
{
    public class FaqCategoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
