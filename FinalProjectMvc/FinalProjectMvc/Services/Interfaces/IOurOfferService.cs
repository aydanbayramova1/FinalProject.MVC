using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.OurOffer;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IOurOfferService
    {
        Task<OurOfferVM> GetByIdAsync(int id);
        Task<OurOfferDetailVM> GetDetailAsync(int id);
        Task<List<OurOfferVM>> GetAllAsync();
        Task CreateAsync(OurOfferCreateVM vm);
        Task<OurOfferEditVM> GetEditAsync(int id);
        Task UpdateAsync(OurOfferEditVM vm);
        Task<OfferImage?> GetOfferImagesAsync();
        Task DeleteAsync(int id);
    }
}
