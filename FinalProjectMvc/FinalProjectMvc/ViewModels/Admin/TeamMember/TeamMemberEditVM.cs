using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.TeamMember
{
    public class TeamMemberEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(60, ErrorMessage = "Full name can be at most 60 characters.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Full name must contain only letters and spaces, no digits or symbols.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [MaxLength(60, ErrorMessage = "Position can be at most 60 characters.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Position must contain only letters and spaces, no digits or symbols.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description can be at most 1000 characters.")]
        public string Description { get; set; }

        public string? ImgUrl { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 3MB.")]
        public IFormFile? Photo { get; set; }
    }
}
