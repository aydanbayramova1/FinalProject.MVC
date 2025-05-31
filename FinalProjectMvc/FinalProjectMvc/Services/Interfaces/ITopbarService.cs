using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Topbar;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ITopbarService
    {
        Task CreateAsync(TopbarCreateVM vm);
        Task<TopbarEditVM> GetEditAsync(int id);
        Task EditAsync(TopbarEditVM vm);
        Task<TopbarDetailVM> GetDetailAsync(int id);
        Task DeleteAsync(int id);
        Task<List<Topbar>> GetAllAsync();
    }
}
