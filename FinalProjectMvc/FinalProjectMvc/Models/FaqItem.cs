namespace FinalProjectMvc.Models
{
    public class FaqItem : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int FaqCategoryId { get; set; }
        public FaqCategory FaqCategory { get; set; }
    }
}
