using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OurOffer
{
    public class OurOfferEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Title cannot contain numbers or special characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required.")]
        [MaxLength(80, ErrorMessage = "Subtitle cannot exceed 80 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Subtitle cannot contain numbers or special characters.")]
        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }
    }
}
