using FinalProjectMvc.ViewModels.Admin.Size;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface ISizeService
    {
        Task<List<SizeVM>> GetAllAsync();
        Task CreateAsync(SizeCreateVM vm);
        Task<List<SelectListItem>> GetSelectListAsync();
    }
}
