using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.TeamBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ITeamBannerService
    {
        Task<List<TeamBannerVM>> GetAllAsync();
        Task<TeamBanner> GetByIdAsync(int id);
        Task CreateAsync(TeamBannerCreateVM model);
        Task EditAsync(TeamBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
