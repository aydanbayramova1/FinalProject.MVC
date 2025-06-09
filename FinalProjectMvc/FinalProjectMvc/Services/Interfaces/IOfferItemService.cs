using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.OfferItem;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IOfferItemService
    {
        Task<List<OfferItemVM>> GetAllAsync();
        Task<OfferItemVM> GetByIdAsync(int id);
        Task<OfferItemDetailVM> GetDetailAsync(int id);
        Task CreateAsync(OfferItemCreateVM vm);
        Task UpdateAsync(OfferItemEditVM vm);
        Task<List<OfferItem>> GetLastThreeOfferItemsAsync();
        Task DeleteAsync(int id);
    }
}
