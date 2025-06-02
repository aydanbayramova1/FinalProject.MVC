using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ContactBannerService : IContactBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ContactBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<ContactBanner>> GetAllAsync()
        {
            return await _context.ContactBanners.ToListAsync();
        }

        public async Task<ContactBanner> GetByIdAsync(int id)
        {
            return await _context.ContactBanners.FindAsync(id);
        }

        public async Task CreateAsync(ContactBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/contactbanner");

            var entity = new ContactBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.ContactBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ContactBannerEditVM model)
        {
            var entity = await _context.ContactBanners.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Contact banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/contactbanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/contactbanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ContactBanners.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Contact banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/contactbanner");
            _context.ContactBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
