using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OurOffer
{
    public class OurOfferEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
