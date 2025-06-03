using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.MenuBanner;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class MenuBannerService : IMenuBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public MenuBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<MenuBannerVM>> GetAllAsync()
        {
            var banners = await _context.MenuBanners.ToListAsync();
            return _mapper.Map<List<MenuBannerVM>>(banners);
        }

        public async Task<MenuBanner> GetByIdAsync(int id)
        {
            return await _context.MenuBanners.FindAsync(id);
        }

        public async Task CreateAsync(MenuBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/menubanner");

            var entity = new MenuBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.MenuBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(MenuBannerEditVM model)
        {
            var entity = await _context.MenuBanners.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Menu banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/menubanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/menubanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MenuBanners.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Menu banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/menubanner");
            _context.MenuBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
