using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Slider;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task CreateAsync(SliderCreateVM slider);
        Task DeleteAsync(Slider slider);
        Task EditAsync(SliderEditVM model);
        Task<Slider> GetByIdAsync(int id);
    }
}
