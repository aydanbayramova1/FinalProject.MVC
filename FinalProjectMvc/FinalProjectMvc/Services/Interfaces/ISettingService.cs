using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Setting;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ISettingService
    {
        Task<bool> ExistsAsync();
        Task<SettingVM?> GetSettingAsync();
        Task CreateAsync(SettingCreateVM model);
        Task DeleteAllAsync();
        Task<SettingEditVM?> GetEditModelAsync();
        Task EditAsync(SettingEditVM model);

    }
}
