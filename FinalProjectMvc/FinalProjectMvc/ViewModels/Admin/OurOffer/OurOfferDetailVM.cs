using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.OfferImage;
using FinalProjectMvc.ViewModels.Admin.OfferItem;

namespace FinalProjectMvc.ViewModels.Admin.OurOffer
{
    public class OurOfferDetailVM
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<OfferImageVM> OfferImages { get; set; } = new();
        public List<OfferItemVM> OfferItems { get; set; } = new();
    }
}
