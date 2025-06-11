using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryDetailVM> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateVM vm);
        Task UpdateAsync(CategoryEditVM vm);
        Task<Category?> GetEntityByIdAsync(int id);
        Task<List<SelectListItem>> GetSelectListAsync();
        Task DeleteAsync(int id);
        Task<Dictionary<int, string>> GetCategoryTypesWithIdAsync();
    }
}
