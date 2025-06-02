using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ReservationBanner;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ReservationBannerService : IReservationBannerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ReservationBannerService(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task<List<ReservationBannerVM>> GetAllAsync()
        {
            var banners = await _context.ReservationBanners.ToListAsync();
            return _mapper.Map<List<ReservationBannerVM>>(banners);
        }

        public async Task<ReservationBanner> GetByIdAsync(int id)
        {
            return await _context.ReservationBanners.FindAsync(id);
        }

        public async Task CreateAsync(ReservationBannerCreateVM model)
        {
            string fileName = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/reservationbanner");

            var entity = new ReservationBanner
            {
                Title = model.Title,
                Img = fileName
            };

            await _context.ReservationBanners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ReservationBannerEditVM model)
        {
            var entity = await _context.ReservationBanners.FindAsync(model.Id);
            if (entity == null) throw new KeyNotFoundException("Reservation banner not found");

            entity.Title = model.Title;

            if (model.Photo != null)
            {
                string newFile = await model.Photo.SaveFileAsync(_env.WebRootPath, "uploads/reservationbanner");
                entity.Img.DeleteFile(_env.WebRootPath, "uploads/reservationbanner");
                entity.Img = newFile;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReservationBanners.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Reservation banner not found");

            entity.Img.DeleteFile(_env.WebRootPath, "uploads/reservationbanner");
            _context.ReservationBanners.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
