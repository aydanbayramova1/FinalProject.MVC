using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class WhyViewComponent : ViewComponent
    {
        private readonly IServiceItemService _serviceItemService;

        public WhyViewComponent(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allItems = await _serviceItemService.GetAllAsync();
            var lastSixItems = allItems
                .OrderByDescending(x => x.Id) 
                .Take(6)
                .ToList();

            return View(lastSixItems);
        }
    }
}
