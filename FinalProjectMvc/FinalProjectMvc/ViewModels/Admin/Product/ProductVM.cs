namespace FinalProjectMvc.ViewModels.Admin.Product
{
    public class ProductVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } 
        public string CategoryTypeName { get; set; }
        public int CategoryTypeId { get; set; }

        public int CategoryId { get; set; }
        public List<string> Sizes { get; set; }
        public string CategoryName { get; set; }

        public string CategorySlug
        {
            get
            {
                return CategoryName?.ToLower().Replace(" ", "-");
            }
        }
    }
}
