using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class OurOffer : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; } 
        [Required]
        public string Description { get; set; }
        public ICollection<OfferItem> OfferItems { get; set; }
        public ICollection<OfferImage> OfferImages { get; set; }
    }
}
