using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using Microsoft.EntityFrameworkCore;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Services.Interfaces;

namespace FinalProjectMvc.Services
{
    public class AboutBannerService : IAboutBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public AboutBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<AboutBanner>> GetAllAsync()
        {
            return await _context.AboutBanners.ToListAsync();
        }

        public async Task<AboutBanner> GetByIdAsync(int id)
        {
            return await _context.AboutBanners.FindAsync(id);
        }

        public async Task CreateAsync(AboutBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/aboutbanner");

            var entity = new AboutBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.AboutBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(AboutBannerEditVM model)
        {
            var entity = await _context.AboutBanners.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/aboutbanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/aboutbanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AboutBanners.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/aboutbanner");
            _context.AboutBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
