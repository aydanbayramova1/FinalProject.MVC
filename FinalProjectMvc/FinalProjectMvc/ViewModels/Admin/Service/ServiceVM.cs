using FinalProjectMvc.ViewModels.Admin.ServiceItem;

namespace FinalProjectMvc.ViewModels.Admin.Service
{
    public class ServiceVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public List<ServiceItemVM> Items { get; set; }
    }
}
