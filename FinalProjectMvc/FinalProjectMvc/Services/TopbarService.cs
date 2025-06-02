using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Topbar;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class TopbarService : ITopbarService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TopbarService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(TopbarCreateVM vm)
        {
            var topbar = _mapper.Map<Topbar>(vm);
            await _context.Topbars.AddAsync(topbar);
            await _context.SaveChangesAsync();
        }

        public async Task<TopbarEditVM> GetEditAsync(int id)
        {
            var topbar = await _context.Topbars.FindAsync(id);
            if (topbar == null) throw new KeyNotFoundException("Topbar not found");
            return _mapper.Map<TopbarEditVM>(topbar);
        }

        public async Task EditAsync(TopbarEditVM vm)
        {
            var topbar = await _context.Topbars.FindAsync(vm.Id);
            if (topbar == null) throw new KeyNotFoundException("Topbar not found");

            _mapper.Map(vm, topbar);
            _context.Topbars.Update(topbar);
            await _context.SaveChangesAsync();
        }

        public async Task<TopbarDetailVM> GetDetailAsync(int id)
        {
            var topbar = await _context.Topbars.FindAsync(id);
            if (topbar == null) throw new KeyNotFoundException("Topbar not found");
            return _mapper.Map<TopbarDetailVM>(topbar);
        }

        public async Task DeleteAsync(int id)
        {
            var topbar = await _context.Topbars.FindAsync(id);
            if (topbar == null) throw new KeyNotFoundException("Topbar not found");
            _context.Topbars.Remove(topbar);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Topbar>> GetAllAsync()
        {
            return await _context.Topbars.ToListAsync();
        }
    }
}
