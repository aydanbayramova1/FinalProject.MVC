using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class ReservationBannerViewComponent : ViewComponent
{
    private readonly IReservationBannerService _reservationBannerService;

    public ReservationBannerViewComponent(IReservationBannerService reservationBannerService)
    {
        _reservationBannerService = reservationBannerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var banner = (await _reservationBannerService.GetAllAsync()).FirstOrDefault();

        if (banner == null || string.IsNullOrEmpty(banner.Img) || string.IsNullOrEmpty(banner.Title))
        {
            return Content("");
        }

        return View(banner);
    }
}
