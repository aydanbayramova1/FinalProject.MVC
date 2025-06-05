using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Header;
using FinalProjectMvc.ViewModels.Admin.Topbar;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectMvc.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ITopbarService _topbarService;
        private readonly ILayoutService _layoutService;
        private readonly IMapper _mapper;

        public HeaderViewComponent(ITopbarService topbarService, ILayoutService layoutService, IMapper mapper)
        {
            _topbarService = topbarService;
            _layoutService = layoutService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topbars = await _topbarService.GetAllAsync();
            var topbarVMs = _mapper.Map<List<TopbarVM>>(topbars);

            var settings = await _layoutService.GetAllSettingsAsync();

            if (!settings.ContainsKey("HeaderLogo") || string.IsNullOrEmpty(settings["HeaderLogo"]))
            {
                settings["HeaderLogo"] = "/assets/images/default-logo.png";
            }

            var headerVM = new HeaderVM
            {
                Settings = settings,
                Topbars = topbarVMs
            };

            return View(headerVM);
        }
    }
}
