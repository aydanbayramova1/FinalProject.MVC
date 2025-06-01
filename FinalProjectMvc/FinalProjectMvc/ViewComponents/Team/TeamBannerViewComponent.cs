using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Team
{
    public class TeamBannerViewComponent : ViewComponent
    {
        private readonly ITeamBannerService _teamBannerService;

        public TeamBannerViewComponent(ITeamBannerService teamBannerService)
        {
            _teamBannerService = teamBannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = (await _teamBannerService.GetAllAsync()).FirstOrDefault();

            if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
            {
                return Content("");
            }

            return View(banner);
        }

    }
}