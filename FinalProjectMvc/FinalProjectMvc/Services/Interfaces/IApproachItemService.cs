using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using FinalProjectMvc.ViewModels.Admin.StoryItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IApproachItemService
    {
        Task<List<ApproachItemVM>> GetAllAsync();
        Task<ApproachItemDetailVM> GetDetailAsync(int id);
        Task CreateAsync(ApproachItemCreateVM vm);
        Task<ApproachItemEditVM> GetEditAsync(int id);
        Task EditAsync(ApproachItemEditVM vm);
        Task DeleteAsync(int id);

    }
}
