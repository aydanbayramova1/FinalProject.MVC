using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class InteractiveViewComponent : ViewComponent
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public InteractiveViewComponent(ICatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var catalogs = await _catalogService.GetAllAsync();
            var vms = _mapper.Map<List<CatalogVM>>(catalogs);

            var latestFour = vms
                .OrderByDescending(x => x.Id)
                .Take(4)
                .ToList();

            return View(latestFour);
        }
    }
    
}
