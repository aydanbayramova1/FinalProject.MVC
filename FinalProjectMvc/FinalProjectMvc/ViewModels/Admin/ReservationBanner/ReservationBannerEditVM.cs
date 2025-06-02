using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ReservationBanner
{
    public class ReservationBannerEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Title { get; set; }


        public string Img { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
