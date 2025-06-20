using FinalProjectMvc.ViewModels.Admin.Size;

namespace FinalProjectMvc.ViewModels.Admin.ProductSize
{
    public class ProductWithSizeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public int CategoryTypeId { get; set; }
        public string CategorySlug { get; set; }
        public List<SizeVM> Sizes { get; set; } = new();
    }
}
