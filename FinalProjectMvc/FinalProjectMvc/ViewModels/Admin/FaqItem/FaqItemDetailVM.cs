using FinalProjectMvc.ViewModels.Admin.FaqCategory;

namespace FinalProjectMvc.ViewModels.Admin.FaqItem
{
    public class FaqItemDetailVM
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public FaqCategoryVM FaqCategory { get; set; }
    }
}
