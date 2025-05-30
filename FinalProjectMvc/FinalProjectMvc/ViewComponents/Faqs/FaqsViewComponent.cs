using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Faqs
{
    public class FaqsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}