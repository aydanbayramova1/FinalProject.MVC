using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OfferItem
{
    public class OfferItemCreateVM
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
