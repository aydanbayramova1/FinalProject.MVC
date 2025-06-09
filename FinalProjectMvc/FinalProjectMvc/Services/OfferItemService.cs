using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OfferItem;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class OfferItemService :IOfferItemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OfferItemService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OfferItem>> GetLastThreeOfferItemsAsync()
        {
            return await _context.OfferItems
                .OrderByDescending(x => x.Id)
                .Take(3)
                .ToListAsync();
        }
        public async Task<List<OfferItemVM>> GetAllAsync()
        {
            var items = await _context.OfferItems.ToListAsync();
            return _mapper.Map<List<OfferItemVM>>(items);
        }

        public async Task<OfferItemVM> GetByIdAsync(int id)
        {
            var item = await _context.OfferItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Offer item not found");
            return _mapper.Map<OfferItemVM>(item);
        }

        public async Task<OfferItemDetailVM> GetDetailAsync(int id)
        {
            var item = await _context.OfferItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Offer item not found");
            return _mapper.Map<OfferItemDetailVM>(item);
        }

        public async Task CreateAsync(OfferItemCreateVM vm)
        {
            var ourOffer = await _context.OurOffers.FirstOrDefaultAsync();
            if (ourOffer == null) throw new KeyNotFoundException("OurOffer must be created before adding items.");

            var entity = _mapper.Map<OfferItem>(vm);
            entity.OurOfferId = ourOffer.Id;

            await _context.OfferItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OfferItemEditVM vm)
        {
            var item = await _context.OfferItems.FindAsync(vm.Id);
            if (item == null) throw new KeyNotFoundException("Offer item not found");

            _mapper.Map(vm, item);
            _context.OfferItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.OfferItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Offer item not found");

            _context.OfferItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
