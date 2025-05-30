using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Menu
{
    public class MenuBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
