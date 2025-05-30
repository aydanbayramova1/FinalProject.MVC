using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.AboutPage
{
    public class TeamViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
