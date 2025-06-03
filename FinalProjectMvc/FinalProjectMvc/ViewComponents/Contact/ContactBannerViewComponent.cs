using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.ViewComponents
{
    public class ContactBannerViewComponent : ViewComponent
    {
        private readonly IContactBannerService _contactBannerService;

        public ContactBannerViewComponent(IContactBannerService contactBannerService)
        {
            _contactBannerService = contactBannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banners = await _contactBannerService.GetAllAsync();
            var banner = banners.FirstOrDefault();
            if (banner == null) return View();

            return View(banner);
        }
    }
}
