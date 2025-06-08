using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutUs;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AboutUsService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public bool HasAny() => _context.AboutUses.Any();
        public async Task<AboutUs> GetAsync()
        {
            return await _context.AboutUses
                .Include(x => x.OpeningHours)
                .Include(x => x.AboutUsItems)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasAnyAsync()
        {
            return await _context.AboutUses.AnyAsync();
        }
        public async Task<AboutUsDetailVM?> GetDetailAsync()
        {
            var about = await GetAsync();
            return about == null ? null : _mapper.Map<AboutUsDetailVM>(about);
        }

        public async Task CreateAsync(AboutUsCreateVM vm)
        {
            if (HasAny()) throw new InvalidOperationException("About Us already exists.");

            var about = _mapper.Map<AboutUs>(vm);

            about.VideoUrl = await vm.VideoFile.SaveFileAsync(_env.WebRootPath, "uploads/videos");
            about.ImageUrl = await vm.ImageFile.SaveFileAsync(_env.WebRootPath, "uploads/images");

            await _context.AboutUses.AddAsync(about);
            await _context.SaveChangesAsync();
        }

        public async Task<AboutUsEditVM?> GetEditAsync(int id)
        {
            var about = await _context.AboutUses.FindAsync(id);
            return about == null ? null : _mapper.Map<AboutUsEditVM>(about);
        }

        public async Task UpdateAsync(AboutUsEditVM vm)
        {
            var about = await _context.AboutUses.FindAsync(vm.Id);
            if (about == null) throw new KeyNotFoundException("About Us not found.");

            var currentImageUrl = about.ImageUrl;
            var currentVideoUrl = about.VideoUrl;

            _mapper.Map(vm, about);

            if (vm.VideoFile != null)
            {
                about.VideoUrl = await vm.VideoFile.SaveFileAsync(_env.WebRootPath, "uploads/videos");
            }
            else
            {
                about.VideoUrl = currentVideoUrl;
            }

            if (vm.ImageFile != null)
            {
                about.ImageUrl = await vm.ImageFile.SaveFileAsync(_env.WebRootPath, "uploads/images");
            }
            else
            {
                about.ImageUrl = currentImageUrl;
            }

            _context.AboutUses.Update(about);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var about = await _context.AboutUses
                .Include(x => x.OpeningHours)
                .Include(x => x.AboutUsItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (about == null) throw new Exception("About Us not found.");

            _context.OpeningHours.RemoveRange(about.OpeningHours);
            _context.AboutUsItems.RemoveRange(about.AboutUsItems);
            _context.AboutUses.Remove(about);

            await _context.SaveChangesAsync();
        }
        public async Task<AboutUs?> GetByIdAsync(int id)
        {
            return await _context.AboutUses
                .Include(x => x.OpeningHours)
                .Include(x => x.AboutUsItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



    }
}