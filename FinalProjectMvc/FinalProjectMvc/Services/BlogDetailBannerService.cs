using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.BlogDetailBanner;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class BlogDetailBannerService : IBlogDetailBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public BlogDetailBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<BlogDetailBannerVM>> GetAllAsync()
        {
            var banners = await _context.BlogDetailBanners.ToListAsync();
            return _mapper.Map<List<BlogDetailBannerVM>>(banners);
        }

        public async Task<BlogDetailBanner> GetByIdAsync(int id)
        {
            return await _context.BlogDetailBanners.FindAsync(id);
        }

        public async Task CreateAsync(BlogDetailBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/blogdetailbanner");

            var entity = new BlogDetailBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.BlogDetailBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(BlogDetailBannerEditVM model)
        {
            var entity = await _context.BlogDetailBanners.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Blog detail banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/blogdetailbanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/blogdetailbanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.BlogDetailBanners.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Blog detail banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/blogdetailbanner");
            _context.BlogDetailBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
