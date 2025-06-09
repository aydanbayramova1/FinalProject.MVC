using FinalProjectMvc.ViewModels.Admin.FaqCategory;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IFaqCategoryService
    {
        Task<List<FaqCategoryVM>> GetAllAsync();
        Task<FaqCategoryDetailVM> GetByIdAsync(int id);
        Task CreateAsync(FaqCategoryCreateVM vm);
        Task<FaqCategoryEditVM> GetEditVMAsync(int id);
        Task UpdateAsync(FaqCategoryEditVM vm);
        Task<List<FaqCategoryDetailVM>> GetAllWithItemsAsync();
        Task DeleteAsync(int id);
    }
}
