using FinalProjectMvc.ViewModels.Admin.OpeningHour;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IOpeningHourService
    {
        Task<OpeningHourVM> GetAsync();
        Task<OpeningHourDetailVM> GetDetailAsync(int id);
        Task<OpeningHourVM> GetByIdAsync(int id); 
        Task<OpeningHourEditVM> GetEditAsync(int id);
        Task CreateAsync(OpeningHourCreateVM vm);
        Task EditAsync(OpeningHourEditVM vm);
        Task DeleteAsync(int id);
        Task<bool> AnyAsync();
        Task<bool> ExistsAsync();
    }
}
