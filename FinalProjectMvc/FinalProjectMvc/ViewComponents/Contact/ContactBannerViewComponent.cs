using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            var banner = (await _contactBannerService.GetAllAsync()).FirstOrDefault();

            if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
            {
                return Content("");
            }

            return View(banner);
        }
    }
}
