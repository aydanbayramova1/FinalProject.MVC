using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OfferImage
{
    public class OfferImageEditVM
    {
        public int OurOfferId { get; set; }
        public string? MainImageUrl { get; set; }
        public string? LeftImageUrl { get; set; }
        public string? RightImageUrl { get; set; }
        public IFormFile? MainImage { get; set; }
        public IFormFile? LeftImage { get; set; }
        public IFormFile? RightImage { get; set; }
    }
}
