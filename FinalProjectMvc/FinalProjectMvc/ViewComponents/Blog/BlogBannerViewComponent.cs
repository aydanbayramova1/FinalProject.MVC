using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Blog
{
    public class BlogBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
