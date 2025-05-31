using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Service;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServiceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Service> GetAsync()
        {
            return await _context.Services
                .Include(s => s.Items)
                .FirstOrDefaultAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateAsync(ServiceCreateVM model)
        {
            var service = _mapper.Map<Service>(model);
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ServiceEditVM model)
        {
            var entity = await _context.Services.FindAsync(model.Id);
            if (entity == null) throw new Exception("Service not found");

            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) throw new Exception("Service not found");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
