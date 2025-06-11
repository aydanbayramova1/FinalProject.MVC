using FinalProjectMvc.ViewModels.Admin.CategoryType;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ICategoryTypeService
    {
        Task<List<CategoryTypeVM>> GetAllAsync();
        Task CreateAsync(CategoryTypeCreateVM vm);
    }
}
