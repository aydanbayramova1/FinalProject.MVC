using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.TeamMember
{
    public class TeamMemberEditVM
    {
        public int Id { get; set; }

        [Required, MaxLength(100), RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only letters allowed")]
        public string FullName { get; set; }

        [Required, MaxLength(100), RegularExpression(@"^[^\d]*$", ErrorMessage = "Position must not contain numbers")]
        public string Position { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        public string? ImgUrl { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
