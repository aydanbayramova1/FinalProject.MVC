using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IServiceItemService
    {
        Task<List<ServiceItem>> GetAllAsync();
        Task<ServiceItem> GetByIdAsync(int id);
        Task CreateAsync(ServiceItemCreateVM model);
        Task EditAsync(ServiceItemEditVM model);
        Task DeleteAsync(int id);
        Task<ServiceItem> GetByIdWithServiceAsync(int id);
    }
}
