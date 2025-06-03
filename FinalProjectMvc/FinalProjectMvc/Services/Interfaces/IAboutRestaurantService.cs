using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutRestaurant;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IAboutRestaurantService
    {
        Task<List<AboutRestaurant>> GetAllAsync();
        Task CreateAsync(AboutRestaurantCreateVM vm);
        Task<AboutRestaurant> GetByIdAsync(int id);
        Task UpdateAsync(AboutRestaurantEditVM vm);
        Task DeleteAsync(int id);
    }
}
