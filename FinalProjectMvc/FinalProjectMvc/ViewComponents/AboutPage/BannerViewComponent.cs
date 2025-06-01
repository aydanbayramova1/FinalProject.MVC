using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.ViewComponents.AboutPage
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly IAboutBannerService _aboutBannerService;

        public BannerViewComponent(IAboutBannerService aboutBannerService)
        {
            _aboutBannerService = aboutBannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = (await _aboutBannerService.GetAllAsync()).FirstOrDefault();

            if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
            {
                return Content("");
            }

            return View(banner);
        }
    }
}
