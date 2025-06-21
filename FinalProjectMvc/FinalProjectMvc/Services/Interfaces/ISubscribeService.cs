using FinalProjectMvc.Models;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task AddAsync(string email);
        Task<List<Subscribe>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<bool> CheckEmailExistsAsync(string email);
    }
}
