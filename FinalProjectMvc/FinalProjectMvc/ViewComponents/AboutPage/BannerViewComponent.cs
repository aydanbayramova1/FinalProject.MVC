using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var banners = await _aboutBannerService.GetAllAsync();
            return View(banners);
        }
    }
}