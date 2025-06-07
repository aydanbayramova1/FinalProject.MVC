using FinalProjectMvc.Models;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IOurStoryService
    {
        Task<OurStory> GetFirstAsync();
        Task<OurStory> GetAsync();
        Task<OurStory> GetByIdAsync(int id);
        Task CreateAsync(OurStory ourStory);
        Task UpdateAsync(OurStory ourStory);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync();
    }
}
