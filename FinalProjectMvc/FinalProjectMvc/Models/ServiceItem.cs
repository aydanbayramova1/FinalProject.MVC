namespace FinalProjectMvc.Models
{
    public class ServiceItem : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Img { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
