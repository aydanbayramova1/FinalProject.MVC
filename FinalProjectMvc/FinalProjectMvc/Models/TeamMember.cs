using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class TeamMember : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Img { get; set; }
    }
}
