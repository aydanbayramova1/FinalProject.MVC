using FinalProjectMvc.ViewModels.Admin.FaqItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IFaqItemService
    {
        Task<List<FaqItemVM>> GetAllAsync();
        Task<FaqItemDetailVM> GetByIdAsync(int id);
        Task CreateAsync(FaqItemCreateVM vm);
        Task UpdateAsync(FaqItemEditVM vm);
        Task DeleteAsync(int id);
    }
}
