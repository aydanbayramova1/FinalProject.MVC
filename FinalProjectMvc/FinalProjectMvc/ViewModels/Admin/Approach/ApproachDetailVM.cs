using FinalProjectMvc.ViewModels.Admin.ApproachItem;

namespace FinalProjectMvc.ViewModels.Admin.Approach
{
    public class ApproachDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }     
        public List<ApproachItemVM> Items { get; set; }
    }
}
