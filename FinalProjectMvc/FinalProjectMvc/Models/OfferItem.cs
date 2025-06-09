using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class OfferItem : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int OurOfferId { get; set; }
        public OurOffer OurOffer { get; set; }
    }
}
