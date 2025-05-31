using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Service;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IServiceService
    {
        Task<Service> GetAsync();                 
        Task<Service> GetByIdAsync(int id);      
        Task CreateAsync(ServiceCreateVM model); 
        Task EditAsync(ServiceEditVM model);     
        Task DeleteAsync(int id);
    }
}
