using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.AboutPage
{
    public class BannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
