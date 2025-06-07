using FinalProjectMvc.Models;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IIntroCounterService
    {
        Task<List<IntroCounter>> GetAllAsync();
        Task<IntroCounter> GetByIdAsync(int id);
        Task CreateAsync(IntroCounter counter);
        Task UpdateAsync(IntroCounter counter);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
