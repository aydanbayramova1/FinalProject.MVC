using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.IntroVideo;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IIntroVideoService
    {
        Task<IntroVideo> GetAsync();
        Task<List<IntroVideo>> GetAllAsync();
        Task CreateAsync(IntroVideoCreateVM vm);
        Task<IntroVideoEditVM> GetEditVMAsync(int id);
        Task EditAsync(IntroVideoEditVM vm);
        Task DeleteAsync(int id);
    }
}
