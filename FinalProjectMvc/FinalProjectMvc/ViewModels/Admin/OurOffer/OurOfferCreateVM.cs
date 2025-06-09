using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OurOffer
{
    public class OurOfferCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
