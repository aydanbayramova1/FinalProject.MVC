using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.OfferImage
{
    public class OfferImageCreateVM
    {
        [Required(ErrorMessage = "Main image is required.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Main image size must not exceed 3 MB.")]
        public IFormFile MainImage { get; set; }

        [Required(ErrorMessage = "Left image is required.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Left image size must not exceed 3 MB.")]
        public IFormFile LeftImage { get; set; }

        [Required(ErrorMessage = "Right image is required.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Right image size must not exceed 3 MB.")]
        public IFormFile RightImage { get; set; }

        [Required]
        public int OurOfferId { get; set; }
    }
}
