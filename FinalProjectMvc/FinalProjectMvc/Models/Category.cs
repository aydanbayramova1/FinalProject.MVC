using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Product> Products { get; set; }
        public int CategoryTypeId { get; set; } 
        public CategoryType CategoryType { get; set; }
    }
}
