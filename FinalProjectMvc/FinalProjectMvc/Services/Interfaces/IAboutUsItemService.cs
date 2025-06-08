using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IAboutUsItemService
    {
        Task<List<AboutUsItemVM>> GetAllAsync();
        Task<AboutUsItemDetailVM> GetDetailAsync(int id);
        Task CreateAsync(AboutUsItemCreateVM vm);
        Task<AboutUsItemEditVM> GetEditAsync(int id);
        Task EditAsync(AboutUsItemEditVM vm);
        Task<AboutUsItem> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
