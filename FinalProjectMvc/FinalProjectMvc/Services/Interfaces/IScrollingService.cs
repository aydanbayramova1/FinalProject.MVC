using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Scrolling;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IScrollingService
    {
        Task<IEnumerable<Scrolling>> GetAllAsync();
        Task<Scrolling> GetByIdAsync(int id);
        Task CreateAsync(ScrollingCreateVM model);
        Task EditAsync(ScrollingEditVM model);
        Task DeleteAsync(Scrolling scrolling);
    }
}
