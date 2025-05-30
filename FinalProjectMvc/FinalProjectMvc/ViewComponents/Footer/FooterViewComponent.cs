using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
