using Microsoft.AspNetCore.Mvc;
using FinalProjectMvc.Services.Interfaces;
using AutoMapper;
using FinalProjectMvc.ViewModels.Admin.Topbar;

namespace FinalProjectMvc.ViewComponents.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ITopbarService _topbarService;
        private readonly IMapper _mapper;

        public HeaderViewComponent(ITopbarService topbarService, IMapper mapper)
        {
            _topbarService = topbarService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topbars = await _topbarService.GetAllAsync();
            var topbarVMs = _mapper.Map<List<TopbarVM>>(topbars);

            return View(topbarVMs);
        }
    }
}
