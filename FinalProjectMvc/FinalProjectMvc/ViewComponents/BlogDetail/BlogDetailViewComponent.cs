using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.BlogDetail
{
    public class BlogDetailViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
