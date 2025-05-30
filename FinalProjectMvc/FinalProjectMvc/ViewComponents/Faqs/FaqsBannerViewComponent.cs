using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Faqs
{
    public class FaqsBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
