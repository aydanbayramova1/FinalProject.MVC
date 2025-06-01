using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Blog
{
    public class BlogBannerViewComponent : ViewComponent
    {
        private readonly IBlogBannerService _blogBannerService;

        public BlogBannerViewComponent(IBlogBannerService blogBannerService)
        {
            _blogBannerService = blogBannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = (await _blogBannerService.GetAllAsync()).FirstOrDefault();

            if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
            {
                return Content("");
            }

            return View(banner);
        }
    }
}