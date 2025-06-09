using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class OfferImage : BaseEntity
    {
        [Required]
        public string ImageUrl { get; set; } = null!;
        [Required]
        public string ImageType { get; set; } = null!;
        public int OurOfferId { get; set; }
        public OurOffer OurOffer { get; set; }
    }
}
