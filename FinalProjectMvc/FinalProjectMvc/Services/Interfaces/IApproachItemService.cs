using FinalProjectMvc.ViewModels.Admin.ApproachItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IApproachItemService
    {
        Task<ApproachItemVM> GetByIdAsync(int id);
        Task CreateAsync(ApproachItemCreateVM vm);
        Task UpdateAsync(int id, ApproachItemEditVM vm);
        Task DeleteAsync(int id);
    }
}
