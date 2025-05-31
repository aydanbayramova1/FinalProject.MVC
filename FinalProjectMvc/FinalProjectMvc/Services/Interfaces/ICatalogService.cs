using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Catalog;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<Catalog>> GetAllAsync();
        Task<Catalog> GetByIdAsync(int id);
        Task CreateAsync(CatalogCreateVM model);
        Task EditAsync(CatalogEditVM model);
        Task DeleteAsync(int id);
    }
}
