using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OfferImage
{
    public class OfferImageCreateVM
    {
        [Required(ErrorMessage = "Main şəkil seçilməlidir.")]
        public IFormFile MainImage { get; set; }

        [Required(ErrorMessage = "Sol şəkil seçilməlidir.")]
        public IFormFile LeftImage { get; set; }

        [Required(ErrorMessage = "Sağ şəkil seçilməlidir.")]
        public IFormFile RightImage { get; set; }

        [Required]
        public int OurOfferId { get; set; }
    }
}
