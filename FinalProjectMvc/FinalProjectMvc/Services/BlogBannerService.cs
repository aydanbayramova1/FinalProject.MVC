using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.BlogBanner;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class BlogBannerService : IBlogBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public BlogBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        public async Task CreateAsync(BlogBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/blogbanner");
            var entity = new BlogBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.BlogBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.BlogBanners.FindAsync(id);
            if (entity == null) throw new Exception("Banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/blogbanner");
            _context.BlogBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(BlogBannerEditVM model)
        {
            var entity = await _context.BlogBanners.FindAsync(model.Id);
            if (entity == null) throw new Exception("Banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/blogbanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/blogbanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<BlogBanner>> GetAllAsync()
        {
            return await _context.BlogBanners.ToListAsync();
        }

        public async Task<BlogBanner> GetByIdAsync(int id)
        {
            return await _context.BlogBanners.FindAsync(id);
        }
    }
}
