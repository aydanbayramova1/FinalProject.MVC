using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Product;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateVM vm);
        Task<ProductCreateVM> GetCreateVMAsync();
        Task<ProductEditVM> GetEditVMAsync(int id);
        Task EditAsync(ProductEditVM vm);
        Task<ProductDetailVM> GetDetailAsync(int id);
        Task<List<ProductVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<bool> ValidateProductAsync(ProductCreateVM vm);
        Task<bool> ValidateProductAsync(ProductEditVM vm);
        Task<string> GetCategoryTypeAsync(int categoryId);
        Task<IQueryable<ProductVM>> GetAllQueryAsync();
        Task<List<Product>> SearchAsync(string query);
    }
}
