using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IAboutBannerService
    {
        Task<List<AboutBannerVM>> GetAllAsync();
        Task<AboutBanner> GetByIdAsync(int id);
        Task CreateAsync(AboutBannerCreateVM model);
        Task EditAsync(AboutBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
