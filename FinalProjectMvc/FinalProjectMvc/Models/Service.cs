namespace FinalProjectMvc.Models
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public ICollection<ServiceItem> Items { get; set; }
    }
}
