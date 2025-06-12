namespace FinalProjectMvc.ViewModels.Admin.MenuProduct
{
    public class MenuProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTypeName { get; set; }
        public List<string>? Sizes { get; set; }
    }
}
