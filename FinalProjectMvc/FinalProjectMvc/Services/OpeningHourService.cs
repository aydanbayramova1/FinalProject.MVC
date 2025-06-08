using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class OpeningHourService : IOpeningHourService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OpeningHourService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OpeningHourVM?> GetAsync()
        {
            var data = await _context.OpeningHours.FirstOrDefaultAsync();
            if (data == null) return null;

            return _mapper.Map<OpeningHourVM>(data);
        }
        public async Task<bool> AnyAsync()
        {
            return await _context.AboutUses.AnyAsync();
        }
        public async Task<OpeningHourDetailVM> GetDetailAsync(int id)
        {
            var entity = await _context.OpeningHours.FindAsync(id);
            return _mapper.Map<OpeningHourDetailVM>(entity);
        }

        public async Task<OpeningHourEditVM> GetEditAsync(int id)
        {
            var entity = await _context.OpeningHours.FindAsync(id);
            return _mapper.Map<OpeningHourEditVM>(entity);
        }

        public async Task CreateAsync(OpeningHourCreateVM vm)
        {
            var aboutUs = await _context.AboutUses.FirstOrDefaultAsync();
            if (aboutUs == null) throw new KeyNotFoundException("AboutUs not found");

            var entity = _mapper.Map<OpeningHour>(vm);
            entity.AboutUsId = aboutUs.Id;

            await _context.OpeningHours.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(OpeningHourEditVM vm)
        {
            var entity = await _context.OpeningHours.FindAsync(vm.Id);
            if (entity == null) throw new KeyNotFoundException("OpeningHour not found");

            _mapper.Map(vm, entity);
            _context.OpeningHours.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.OpeningHours.FindAsync(id);
            if (entity != null)
            {
                _context.OpeningHours.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync()
        {
            return await _context.OpeningHours.AnyAsync();
        }
        public async Task<OpeningHourVM> GetByIdAsync(int id)
        {
            var entity = await _context.OpeningHours.FindAsync(id);
            return _mapper.Map<OpeningHourVM>(entity);
        }

    }
}