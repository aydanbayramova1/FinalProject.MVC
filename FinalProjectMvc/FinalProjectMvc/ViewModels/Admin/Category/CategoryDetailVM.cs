using FinalProjectMvc.ViewModels.Admin.Product;

namespace FinalProjectMvc.ViewModels.Admin.Category
{
    public class CategoryDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string CategoryTypeName { get; set; }
        public int CategoryTypeId { get; set; }
        public bool IsDrinkCategory => CategoryTypeName == "Drink";
    }
}
