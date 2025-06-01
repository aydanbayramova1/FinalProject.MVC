using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class MenuBannerViewComponent : ViewComponent
{
    private readonly IMenuBannerService _menuBannerService;

    public MenuBannerViewComponent(IMenuBannerService menuBannerService)
    {
        _menuBannerService = menuBannerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var banner = (await _menuBannerService.GetAllAsync()).FirstOrDefault();

        if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
        {
            return Content("");
        }

        return View(banner);
    }
}
