using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.ProductSize;

namespace FinalProjectMvc.ViewModels.Admin.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTypeName { get; set; }
        public List<string>? Sizes { get; set; }
    }
}
