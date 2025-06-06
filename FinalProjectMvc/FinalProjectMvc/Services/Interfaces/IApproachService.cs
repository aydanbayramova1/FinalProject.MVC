using FinalProjectMvc.ViewModels.Admin.Approach;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IApproachService
    {
        Task<IEnumerable<ApproachVM>> GetAsync();
        Task<ApproachVM> GetByIdAsync(int id); 
        Task CreateAsync(ApproachCreateVM vm);
        Task UpdateAsync(int id, ApproachEditVM vm);
        Task DeleteAsync(int id);
        Task<bool> HasAnyAsync();
    }
}
