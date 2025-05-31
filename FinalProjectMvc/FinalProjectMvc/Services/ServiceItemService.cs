using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ServiceItem;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ServiceItemService : IServiceItemService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ServiceItemService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<ServiceItem>> GetAllAsync()
        {
            return await _context.ServiceItems.Include(x => x.Service).ToListAsync();
        }

        public async Task<ServiceItem> GetByIdAsync(int id)
        {
            return await _context.ServiceItems.FindAsync(id);
        }

        public async Task CreateAsync(ServiceItemCreateVM model)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
            string path = Path.Combine(_env.WebRootPath, "uploads/services", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var entity = _mapper.Map<ServiceItem>(model);
            entity.Img = fileName;

            await _context.ServiceItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ServiceItemEditVM model)
        {
            var entity = await _context.ServiceItems.FindAsync(model.Id);
            if (entity == null) throw new Exception("ServiceItem not found");

            entity.Title = model.Title;
            entity.Subtitle = model.Subtitle;

            if (model.Photo != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.Photo.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/services", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(entity.Img))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "uploads/services", entity.Img);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                entity.Img = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ServiceItems.FindAsync(id);
            if (entity == null) throw new Exception("ServiceItem not found");

            string path = Path.Combine(_env.WebRootPath, "uploads/services", entity.Img);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _context.ServiceItems.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
