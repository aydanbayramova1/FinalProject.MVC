using FinalProjectMvc.ViewModels.Admin.StoryItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IStoryItemService
    {
        Task<List<StoryItemVM>> GetAllAsync();
        Task<StoryItemDetailVM> GetDetailAsync(int id);
        Task CreateAsync(StoryItemCreateVM vm);
        Task<StoryItemEditVM> GetEditAsync(int id);
        Task EditAsync(StoryItemEditVM vm);
        Task DeleteAsync(int id);
    }
}
