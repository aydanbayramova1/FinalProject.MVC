using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class ScrollingViewComponent : ViewComponent
    {
        private readonly IScrollingService _scrollingService;

        public ScrollingViewComponent(IScrollingService scrollingService)
        {
            _scrollingService = scrollingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scrollings = await _scrollingService.GetAllAsync();
            return View(scrollings);
        }
    }
}
