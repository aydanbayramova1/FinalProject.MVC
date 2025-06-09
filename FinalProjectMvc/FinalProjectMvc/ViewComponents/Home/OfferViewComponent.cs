using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OfferImage;
using FinalProjectMvc.ViewModels.Admin.OfferItem;
using FinalProjectMvc.ViewModels.Admin.OurOffer;
using Microsoft.AspNetCore.Mvc;
using System.IO;

public class OfferViewComponent : ViewComponent
{
    private readonly IOurOfferService _service;
    private readonly IOfferItemService _offerItemService;
    private readonly IOfferImageService _offerImageService;
    private readonly IMapper _mapper;

    public OfferViewComponent(
        IOfferItemService offerItemService,
        IOfferImageService offerImageService,
        IOurOfferService service,
        IMapper mapper)
    {
        _offerItemService = offerItemService;
        _offerImageService = offerImageService;
        _service = service;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var offer = (await _service.GetAllAsync()).FirstOrDefault();
        if (offer == null)
            return Content(string.Empty);

        var offerVM = _mapper.Map<OurOfferVM>(offer);
        offerVM.OfferItems = _mapper.Map<List<OfferItemVM>>(await _offerItemService.GetLastThreeOfferItemsAsync());
        offerVM.OfferImages = _mapper.Map<List<OfferImageVM>>(await _offerImageService.GetAllAsync());

        bool hasItems = offerVM.OfferItems != null && offerVM.OfferItems.Any();
        bool hasRealImages = false;

        if (offerVM.OfferImages != null)
        {
            string imageRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "offerimages");

            foreach (var img in offerVM.OfferImages)
            {
                if (!string.IsNullOrWhiteSpace(img.MainImageUrl) &&
                    System.IO.File.Exists(Path.Combine(imageRoot, img.MainImageUrl)))
                {
                    hasRealImages = true;
                    break;
                }

                if (!string.IsNullOrWhiteSpace(img.LeftImageUrl) &&
                    System.IO.File.Exists(Path.Combine(imageRoot, img.LeftImageUrl)))
                {
                    hasRealImages = true;
                    break;
                }

                if (!string.IsNullOrWhiteSpace(img.RightImageUrl) &&
                    System.IO.File.Exists(Path.Combine(imageRoot, img.RightImageUrl)))
                {
                    hasRealImages = true;
                    break;
                }
            }
        }


        if (!hasItems || !hasRealImages)
            return Content(string.Empty);

        return View(offerVM);
    }

}
