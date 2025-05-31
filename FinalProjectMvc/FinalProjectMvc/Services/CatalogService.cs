using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Catalog;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CatalogService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<Catalog>> GetAllAsync()
        {
            return await _context.Catalogs.ToListAsync();
        }

        public async Task<Catalog> GetByIdAsync(int id)
        {
            return await _context.Catalogs.FindAsync(id);
        }

        public async Task CreateAsync(CatalogCreateVM model)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            string path = Path.Combine(_env.WebRootPath, "uploads/catalogs", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            Catalog catalog = _mapper.Map<Catalog>(model);
            catalog.Img = fileName;

            await _context.Catalogs.AddAsync(catalog);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(CatalogEditVM model)
        {
            var entity = await _context.Catalogs.FindAsync(model.Id);
            if (entity == null)
                throw new KeyNotFoundException("Catalog not found");

            entity.Title = model.Title;
            entity.Description = model.Description;

            if (model.Photo != null)
            {
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                string newPath = Path.Combine(_env.WebRootPath, "uploads/catalogs", newFileName);

                using var stream = new FileStream(newPath, FileMode.Create);
                await model.Photo.CopyToAsync(stream);

                string oldPath = Path.Combine(_env.WebRootPath, "uploads/catalogs", entity.Img);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                entity.Img = newFileName;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Catalogs.FindAsync(id);
            if (entity == null) throw new Exception("Catalog not found");

            string path = Path.Combine(_env.WebRootPath, "uploads/catalogs", entity.Img);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Catalogs.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}
