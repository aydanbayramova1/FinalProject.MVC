using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.BlogBanner;
using FinalProjectMvc.ViewModels.Admin.ReservationBanner;
using FinalProjectMvc.ViewModels.Admin.TeamBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IReservationBannerService
    {
        Task<List<ReservationBannerVM>> GetAllAsync();
        Task<ReservationBanner> GetByIdAsync(int id);
        Task CreateAsync(ReservationBannerCreateVM model);
        Task EditAsync(ReservationBannerEditVM model);
        Task DeleteAsync(int id);

    }
}
