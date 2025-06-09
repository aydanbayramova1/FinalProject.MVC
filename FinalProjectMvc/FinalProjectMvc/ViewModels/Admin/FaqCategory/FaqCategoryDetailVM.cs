using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.FaqItem;

namespace FinalProjectMvc.ViewModels.Admin.FaqCategory
{
    public class FaqCategoryDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<FaqItemVM> FaqItems { get; set; }
    }
}
