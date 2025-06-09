using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.OfferItem
{
    public class OfferItemEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
