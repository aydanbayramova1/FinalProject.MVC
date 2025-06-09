using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OurOffer;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class OurOfferService : IOurOfferService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OurOfferService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OurOfferVM>> GetAllAsync()
        {
            var entities = await _context.OurOffers
                .Include(x => x.OfferItems)
                .Include(x => x.OfferImages)
                .ToListAsync();

            return _mapper.Map<List<OurOfferVM>>(entities);
        }
   

        public async Task<OfferImage?> GetOfferImagesAsync()
        {
            return await _context.OfferImages
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(); 
        }
        public async Task<OurOfferVM> GetByIdAsync(int id)
        {
            var entity = await _context.OurOffers
                .Include(x => x.OfferItems)
                .Include(x => x.OfferImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : _mapper.Map<OurOfferVM>(entity);
        }

        public async Task<OurOfferDetailVM> GetDetailAsync(int id)
        {
            var entity = await _context.OurOffers
                .Include(x => x.OfferItems)
                .Include(x => x.OfferImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : _mapper.Map<OurOfferDetailVM>(entity);
        }

        public async Task CreateAsync(OurOfferCreateVM vm)
        {
            var entity = _mapper.Map<OurOffer>(vm);
            await _context.OurOffers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<OurOfferEditVM> GetEditAsync(int id)
        {
            var entity = await _context.OurOffers.FindAsync(id);
            return entity == null ? null : _mapper.Map<OurOfferEditVM>(entity);
        }

        public async Task UpdateAsync(OurOfferEditVM vm)
        {
            var entity = await _context.OurOffers.FindAsync(vm.Id);
            if (entity == null) return;

            _mapper.Map(vm, entity);
            _context.OurOffers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.OurOffers
                .Include(x => x.OfferItems)
                .Include(x => x.OfferImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return;

            _context.OfferItems.RemoveRange(entity.OfferItems);
            _context.OfferImages.RemoveRange(entity.OfferImages);
            _context.OurOffers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
