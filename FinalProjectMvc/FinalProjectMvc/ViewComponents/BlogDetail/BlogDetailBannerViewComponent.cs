using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.BlogDetail
{
    public class BlogDetailBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}