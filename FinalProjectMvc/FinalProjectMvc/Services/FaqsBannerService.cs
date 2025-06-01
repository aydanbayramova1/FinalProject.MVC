using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.FaqsBanner;
using Microsoft.EntityFrameworkCore;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Services.Interfaces;

namespace FinalProjectMvc.Services
{
    public class FaqsBannerService : IFaqsBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public FaqsBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<FaqsBanner>> GetAllAsync()
        {
            return await _context.FaqsBanners.ToListAsync();
        }

        public async Task<FaqsBanner> GetByIdAsync(int id)
        {
            return await _context.FaqsBanners.FindAsync(id);
        }

        public async Task CreateAsync(FaqsBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/faqsbanner");

            var entity = new FaqsBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.FaqsBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(FaqsBannerEditVM model)
        {
            var entity = await _context.FaqsBanners.FindAsync(model.Id);
            if (entity == null) throw new Exception("Banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/faqsbanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/faqsbanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FaqsBanners.FindAsync(id);
            if (entity == null) throw new Exception("Banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/faqsbanner");
            _context.FaqsBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
