using FinalProjectMvc.ViewModels.Admin.Setting;

namespace FinalProjectMvc.ViewModels.Admin.FaqCategory
{
    public class FaqSectionVM
    {
        public List<FaqCategoryDetailVM> FaqCategories { get; set; }
        public SettingVM Setting { get; set; }
    }
}
