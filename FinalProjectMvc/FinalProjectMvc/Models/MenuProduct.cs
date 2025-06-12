using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMvc.Models
{
    public class MenuProduct : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<MenuProductSize>? MenuProductSizes { get; set; }
    }
}
