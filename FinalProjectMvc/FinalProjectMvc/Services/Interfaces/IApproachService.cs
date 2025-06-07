using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Approach;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IApproachService
    {
        Task<List<ApproachVM>> GetAllAsync();
        Task<ApproachDetailVM> GetByIdAsync(int id);
        Task CreateAsync(ApproachCreateVM model);
        Task UpdateAsync(ApproachEditVM model);
        Task DeleteAsync(int id);
        Task<bool> IsExistsAsync();
    }
}
