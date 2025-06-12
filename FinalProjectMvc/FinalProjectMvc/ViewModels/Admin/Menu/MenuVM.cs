using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.MenuProduct;
using FinalProjectMvc.ViewModels.Admin.Product;
using FinalProjectMvc.ViewModels.Admin.Size;

namespace FinalProjectMvc.ViewModels.Admin.Menu
{
    public class MenuVM
    {
        public List<CategoryVM> Categories { get; set; }
        public List<MenuProductVM> MenuProductVMs { get; set; }
        public List<SizeVM> SizeVMs { get; set; }
    }
}
