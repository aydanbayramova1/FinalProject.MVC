using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.TeamBanner;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class TeamBannerService : ITeamBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public TeamBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<TeamBannerVM>> GetAllAsync()
        {
            var banners = await _context.TeamBanners.ToListAsync();
            return _mapper.Map<List<TeamBannerVM>>(banners);
        }

        public async Task<TeamBanner> GetByIdAsync(int id)
        {
            return await _context.TeamBanners.FindAsync(id);
        }

        public async Task CreateAsync(TeamBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/teambanner");

            var entity = new TeamBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.TeamBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TeamBannerEditVM model)
        {
            var entity = await _context.TeamBanners.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Team banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/teambanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/teambanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TeamBanners.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Team banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/teambanner");
            _context.TeamBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
