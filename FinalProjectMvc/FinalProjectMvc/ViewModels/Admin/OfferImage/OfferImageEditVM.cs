using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;

namespace FinalProjectMvc.ViewModels.Admin.OfferImage
{
    public class OfferImageEditVM
    {
        [Required]
        public int OurOfferId { get; set; }

        public string? MainImageUrl { get; set; }
        public string? LeftImageUrl { get; set; }
        public string? RightImageUrl { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Main image size must not exceed 3 MB.")]
        public IFormFile? MainImage { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Left image size must not exceed 3 MB.")]
        public IFormFile? LeftImage { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Right image size must not exceed 3 MB.")]
        public IFormFile? RightImage { get; set; }
    }
}
