namespace FinalProjectMvc.ViewModels.Admin.ServiceItem
{
    public class ServiceItemEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Img { get; set; }
    }
}
