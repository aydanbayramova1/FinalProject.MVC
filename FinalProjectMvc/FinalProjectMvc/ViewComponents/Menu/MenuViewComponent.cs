using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
