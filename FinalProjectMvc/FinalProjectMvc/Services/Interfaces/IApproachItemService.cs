using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IApproachItemService
    {
        Task<List<ApproachItemDetailVM>> GetAllAsync();
        Task<List<ApproachItemDetailVM>> GetByApproachIdAsync(int approachId);
        Task<ApproachItemDetailVM> GetByIdAsync(int id);
        Task CreateAsync(ApproachItemCreateVM model);
        Task UpdateAsync(ApproachItemEditVM model);
        Task DeleteAsync(int id);

    }
}
