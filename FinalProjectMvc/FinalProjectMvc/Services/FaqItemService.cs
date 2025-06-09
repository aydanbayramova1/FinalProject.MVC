using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.FaqItem;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class FaqItemService : IFaqItemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FaqItemService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FaqItemVM>> GetAllAsync()
        {
            var items = await _context.FaqItems.ToListAsync();
            return _mapper.Map<List<FaqItemVM>>(items);
        }

        public async Task<FaqItemDetailVM> GetByIdAsync(int id)
        {
            var item = await _context.FaqItems
                .Include(x => x.FaqCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return null;

            return _mapper.Map<FaqItemDetailVM>(item);
        }

        public async Task CreateAsync(FaqItemCreateVM vm)
        {
            var item = _mapper.Map<FaqItem>(vm);
            await _context.FaqItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FaqItemEditVM vm)
        {
            var item = await _context.FaqItems.FindAsync(vm.Id);
            if (item == null) return;

            _mapper.Map(vm, item);
            _context.FaqItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.FaqItems.FindAsync(id);
            if (item == null) return;

            _context.FaqItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
