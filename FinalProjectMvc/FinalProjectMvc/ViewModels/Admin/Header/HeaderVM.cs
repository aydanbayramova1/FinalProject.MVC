using FinalProjectMvc.ViewModels.Admin.Topbar;

namespace FinalProjectMvc.ViewModels.Admin.Header
{
    public class HeaderVM
    {
        public Dictionary<string, string> Settings { get; set; }
        public List<TopbarVM> Topbars { get; set; }
        public string HeaderLogo { get; set; }
    }
}
