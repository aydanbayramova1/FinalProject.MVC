namespace FinalProjectMvc.ViewModels.Admin.MenuProduct
{
    public class MenuProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string CategoryTypeName { get; set; }

        public int CategoryId { get; set; }
        public List<string> Sizes { get; set; }
    }
}
