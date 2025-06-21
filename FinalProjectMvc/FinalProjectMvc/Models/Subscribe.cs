using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class Subscribe :BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
