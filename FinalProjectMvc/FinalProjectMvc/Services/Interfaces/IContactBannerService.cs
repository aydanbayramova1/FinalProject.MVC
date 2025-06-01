using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IContactBannerService
    {
        Task<List<ContactBanner>> GetAllAsync();
        Task<ContactBanner> GetByIdAsync(int id);
        Task CreateAsync(ContactBannerCreateVM model);
        Task EditAsync(ContactBannerEditVM model);
        Task DeleteAsync(int id);
    }
}
