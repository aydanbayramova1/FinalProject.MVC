using FinalProjectMvc.ViewModels.Admin.OfferImage;
using FinalProjectMvc.ViewModels.Admin.OfferItem;

namespace FinalProjectMvc.ViewModels.Admin.OurOffer
{
    public class OfferVM
    {
        public List<OfferItemVM> OfferItems { get; set; }
        public List<OfferImageVM> OfferImages { get; set; }
    }
}
