using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.MenuBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IMenuBannerService
    {
        Task<List<MenuBannerVM>> GetAllAsync();
        Task<MenuBanner> GetByIdAsync(int id);
        Task CreateAsync(MenuBannerCreateVM model);
        Task EditAsync(MenuBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
