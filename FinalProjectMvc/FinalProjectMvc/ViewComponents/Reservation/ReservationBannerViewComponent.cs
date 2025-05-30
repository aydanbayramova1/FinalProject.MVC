using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Reservation
{
    public class ReservationBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
