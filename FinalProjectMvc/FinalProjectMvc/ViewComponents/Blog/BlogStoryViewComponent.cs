using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Blog
{
    public class BlogStoryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}