using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Blog
{
    public class BlogDetailBannerViewComponent : ViewComponent
    {
        private readonly IBlogDetailBannerService _blogDetailBannerService;

        public BlogDetailBannerViewComponent(IBlogDetailBannerService blogDetailBannerService)
        {
            _blogDetailBannerService = blogDetailBannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = (await _blogDetailBannerService.GetAllAsync()).FirstOrDefault();

            if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
            {
                return Content("");
            }

            return View(banner);
        }
    }
}
