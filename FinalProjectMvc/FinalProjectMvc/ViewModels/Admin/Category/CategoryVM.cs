namespace FinalProjectMvc.ViewModels.Admin.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string CategoryTypeName { get; set; }
        public int ProductCount { get; set; }
        public int CategoryTypeId { get; set; }
    }
}
