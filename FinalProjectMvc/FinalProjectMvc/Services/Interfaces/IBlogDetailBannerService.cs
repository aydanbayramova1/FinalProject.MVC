using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using FinalProjectMvc.ViewModels.Admin.BlogDetailBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IBlogDetailBannerService
    {
        Task<List<BlogDetailBanner>> GetAllAsync();
        Task<BlogDetailBanner> GetByIdAsync(int id);
        Task CreateAsync(BlogDetailBannerCreateVM model);
        Task EditAsync(BlogDetailBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
