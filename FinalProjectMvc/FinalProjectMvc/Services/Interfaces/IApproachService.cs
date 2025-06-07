using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Approach;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IApproachService
    {
        Task<Approach> GetFirstAsync();
        Task<Approach> GetAsync();
        Task<Approach> GetByIdAsync(int id);
        Task CreateAsync(Approach ourStory);
        Task UpdateAsync(Approach ourStory);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync();
    }
}
