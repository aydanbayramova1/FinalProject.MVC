using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutUs;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<AboutUs?> GetAsync();
        Task<AboutUsDetailVM?> GetDetailAsync();
        Task CreateAsync(AboutUsCreateVM vm);
        Task<AboutUsEditVM?> GetEditAsync(int id);
        Task UpdateAsync(AboutUsEditVM vm);
        Task DeleteAsync(int id);
        Task<AboutUs> GetByIdAsync(int id);
        Task<bool> HasAnyAsync();
        bool HasAny();
    }
}
