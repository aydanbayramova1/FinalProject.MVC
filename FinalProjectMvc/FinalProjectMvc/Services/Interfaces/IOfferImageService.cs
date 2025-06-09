using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.OfferImage;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IOfferImageService
    {
        Task<List<OfferImageVM>> GetAllAsync();
        Task<OfferImageDetailVM?> GetDetailAsync();
        Task<OfferImageDetailVM?> GetImageDetailByIdAsync(int id);
        Task<OfferImage?> GetByIdAsync(int id);
        Task<OfferImageEditVM?> GetEditAsync();
        Task CreateAsync(OfferImageCreateVM vm);
        Task EditAsync(OfferImageEditVM vm);
        Task<OurOffer?> GetOurOfferAsync();
    
        Task DeleteAsync();
    }
}
