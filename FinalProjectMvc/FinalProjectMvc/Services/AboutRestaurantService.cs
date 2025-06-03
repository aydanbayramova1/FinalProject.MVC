using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutRestaurant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class AboutRestaurantService : IAboutRestaurantService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public AboutRestaurantService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<AboutRestaurant>> GetAllAsync()
        {
            return await _context.AboutRestaurants.ToListAsync();
        }

        public async Task<AboutRestaurant> GetByIdAsync(int id)
        {
            return await _context.AboutRestaurants.FindAsync(id);
        }

        public async Task CreateAsync(AboutRestaurantCreateVM vm)
        {
            if (await _context.AboutRestaurants.AnyAsync())
                throw new InvalidOperationException("Only one AboutRestaurant record is allowed.");

            var entity = _mapper.Map<AboutRestaurant>(vm);

            if (vm.Photo != null)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "img");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.Photo.FileName);
                string fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await vm.Photo.CopyToAsync(stream);

                entity.Image = fileName;
            }

            await _context.AboutRestaurants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AboutRestaurantEditVM vm)
        {
            var entity = await _context.AboutRestaurants.FindAsync(vm.Id);
            if (entity == null) throw new Exception("Not found");

            _mapper.Map(vm, entity);

            if (vm.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img", entity.Image);
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                string folderPath = Path.Combine(_env.WebRootPath, "img");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await vm.ImageFile.CopyToAsync(stream);

                entity.Image = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AboutRestaurants.FindAsync(id);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    string imagePath = Path.Combine(_env.WebRootPath, "img", entity.Image);
                    if (File.Exists(imagePath)) File.Delete(imagePath);
                }

                _context.AboutRestaurants.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
