namespace FinalProjectMvc.Models
{
    public class FaqCategory : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<FaqItem> FaqItems { get; set; }
    }
}
