using FinalProjectMvc.ViewModels.Admin.MenuProduct;
using FinalProjectMvc.ViewModels.Admin.Product;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IMenuProductService
    {
        Task<List<MenuProductVM>> GetAllAsync();
        Task<MenuProductDetailVM> GetDetailAsync(int id);
        Task<MenuProductCreateVM> GetCreateVMAsync();
        Task<MenuProductEditVM> GetEditVMAsync(int id);
        Task CreateAsync(MenuProductCreateVM vm);
        Task EditAsync(MenuProductEditVM vm);
        Task DeleteAsync(int id);
        Task<bool> ValidateMenuProductAsync(MenuProductCreateVM vm);
        Task<bool> ValidateMenuProductAsync(MenuProductEditVM vm);
        Task<string> GetCategoryTypeAsync(int categoryId);
        Task<IQueryable<MenuProductVM>> GetAllQueryAsync();

    }
}
