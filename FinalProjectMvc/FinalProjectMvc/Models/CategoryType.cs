using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class CategoryType : BaseEntity
    {
        [Required]
        public string Name { get; set; } 
        public ICollection<Category> Categories { get; set; }
    }
}
