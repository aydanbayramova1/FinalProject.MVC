using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Table;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ITableService
    {
        Task<List<TableVM>> GetAllAsync();
        Task<TableDetailVM> GetByIdAsync(int id);
        Task CreateAsync(TableCreateVM vm);
        Task DeleteAsync(int id);
    }
}
