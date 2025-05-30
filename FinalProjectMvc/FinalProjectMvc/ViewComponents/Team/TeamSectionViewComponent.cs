using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Team
{
    public class TeamSectionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}