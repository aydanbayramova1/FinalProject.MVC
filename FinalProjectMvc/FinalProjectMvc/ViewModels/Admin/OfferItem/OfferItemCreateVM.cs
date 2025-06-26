using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OfferItem
{
    public class OfferItemCreateVM
    {

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(60, ErrorMessage = "Title cannot exceed 60 characters.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title can only contain letters and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; }
    }
}
