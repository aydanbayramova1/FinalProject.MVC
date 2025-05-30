using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Team
{
    public class TeamBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }

}