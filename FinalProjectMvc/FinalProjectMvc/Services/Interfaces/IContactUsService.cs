using FinalProjectMvc.ViewModels.Admin.ContactUs;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IContactUsService
    {
        Task<IEnumerable<ContactUsVM>> GetAllAsync();
        Task<ContactUsDetailVM> GetByIdAsync(int id);
        Task CreateAsync(ContactUsCreateVM vm);
        Task EditAsync(ContactUsEditVM vm);
        Task DeleteAsync(int id);
    }
}
