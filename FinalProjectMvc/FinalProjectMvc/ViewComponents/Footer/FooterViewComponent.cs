using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.ViewComponents.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _layoutService.GetAllSettingsAsync();

            // Set Default for Footer Background if not found
            if (!settings.ContainsKey("FooterBg") || string.IsNullOrEmpty(settings["FooterBg"]))
            {
                settings["FooterBg"] = "/assets/images/common/footer-bg-image.jpg";
            }

            return View(settings);
        }
    }
}
