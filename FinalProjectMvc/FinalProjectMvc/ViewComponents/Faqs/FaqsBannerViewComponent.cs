using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMvc.ViewComponents
{
    public class FaqsBannerViewComponent : ViewComponent
    {
        private readonly IFaqsBannerService _faqsBannerService;

        public FaqsBannerViewComponent(IFaqsBannerService faqsBannerService)
        {
            _faqsBannerService = faqsBannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = (await _faqsBannerService.GetAllAsync()).FirstOrDefault();

            if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
            {
                return Content("");
            }

            return View(banner);
        }
    }
}
