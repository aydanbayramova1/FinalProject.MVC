using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.FaqsBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IFaqsBannerService
    {
        Task<List<FaqsBannerVM>> GetAllAsync();
        Task<FaqsBanner> GetByIdAsync(int id);
        Task CreateAsync(FaqsBannerCreateVM model);
        Task EditAsync(FaqsBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
