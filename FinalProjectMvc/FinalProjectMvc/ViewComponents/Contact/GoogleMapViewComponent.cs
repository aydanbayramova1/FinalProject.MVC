using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Contact
{
    public class GoogleMapViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}