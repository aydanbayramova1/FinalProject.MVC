using FinalProjectMvc.ViewModels.Admin.ContactMessage;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IContactMessageService
    {
        Task CreateAsync(ContactMessageCreateVM vm);
        Task<IEnumerable<ContactMessageVM>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<ContactMessageVM> GetByIdAsync(int id);
        Task ReplyToMessageAsync(ContactMessageReplyVM vm);
        Task CleanupOldRepliedMessagesAsync();
    }
}
