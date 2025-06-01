using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using FinalProjectMvc.ViewModels.Admin.BlogBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IBlogBannerService
    {
        Task<List<BlogBanner>> GetAllAsync();
        Task<BlogBanner> GetByIdAsync(int id);
        Task CreateAsync(BlogBannerCreateVM model);
        Task EditAsync(BlogBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
