using FinalProjectMvc.ViewModels.Admin.Blog;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllAsync();
        Task<BlogDetailVM> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateVM vm);
        Task UpdateAsync(int id, BlogEditVM vm);
        Task DeleteAsync(int id);
        IEnumerable<BlogVM> GetAllBlogs();

    }
}
