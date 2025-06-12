using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class Size : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<MenuProduct> MenuProducts { get; set; }
    }
}
