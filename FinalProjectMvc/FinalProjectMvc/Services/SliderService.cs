using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Slider;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace FinalProjectMvc.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.AsNoTracking().OrderByDescending(s => s.Id).ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) throw new KeyNotFoundException("Slider tapılmadı!");
            return slider;
        }

        public async Task CreateAsync(SliderCreateVM model)
        {
            if (model.Image == null || model.Image.Length == 0)
                throw new ArgumentNullException("Image", "Şəkil seçilməyib!");

            string fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var slider = _mapper.Map<Slider>(model);
            slider.Img = fileName;
            slider.CreateDate = DateTime.Now;

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(SliderEditVM model)
        {
            var slider = await GetByIdAsync(model.Id);

            slider.Title = model.Title;
            slider.Subtitle = model.Subtitle;
            slider.Description = model.Description;

            if (model.Photo != null && model.Photo.Length > 0)
            {
                string oldPath = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.Img);
                if (File.Exists(oldPath))
                    File.Delete(oldPath);

                string newFileName = $"{Guid.NewGuid()}_{model.Photo.FileName}";
                string newPath = Path.Combine(_env.WebRootPath, "uploads/sliders", newFileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                slider.Img = newFileName;
            }

            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider slider)
        {
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.Img);
            if (File.Exists(path))
                File.Delete(path);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }
    }
}
