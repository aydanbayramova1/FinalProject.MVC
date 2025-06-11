using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.Product;

namespace FinalProjectMvc.ViewModels.Admin.Pricing
{
    public class PricingVM
    {
        public List<CategoryVM> Categories { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}
