using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Scrolling;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ScrollingService : IScrollingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ScrollingService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IEnumerable<Scrolling>> GetAllAsync()
        {
            return await _context.Scrollings.AsNoTracking().OrderByDescending(s => s.Id).ToListAsync();
        }
        public async Task CreateAsync(ScrollingCreateVM model)
        {
            string folder = Path.Combine(_env.WebRootPath, "uploads", "scrollings");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var scrolling = _mapper.Map<Scrolling>(model);
            scrolling.Img = fileName;
            await _context.Scrollings.AddAsync(scrolling);
            await _context.SaveChangesAsync();
        }

        public async Task<Scrolling> GetByIdAsync(int id)
        {
            var scrolling = await _context.Scrollings.FindAsync(id);
            if (scrolling == null)throw new KeyNotFoundException("Scrolling item not found!");
            return scrolling;
        }

        public async Task EditAsync(ScrollingEditVM model)
        {
            var scrolling = await GetByIdAsync(model.Id);
            scrolling.Name = model.Name;
            if (model.Photo != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads", "scrollings");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string oldPath = Path.Combine(folder, scrolling.Img);
                if (File.Exists(oldPath)) File.Delete(oldPath);

                string newFileName = $"{Guid.NewGuid()}_{model.Photo.FileName}";
                string newPath = Path.Combine(folder, newFileName);

                using var stream = new FileStream(newPath, FileMode.Create);
                await model.Photo.CopyToAsync(stream);

                scrolling.Img = newFileName;
            }

            _context.Scrollings.Update(scrolling);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var scrolling = await GetByIdAsync(id);  

            string path = Path.Combine(_env.WebRootPath, "uploads", "scrollings", scrolling.Img);
            if (File.Exists(path)) File.Delete(path);

            _context.Scrollings.Remove(scrolling);
            await _context.SaveChangesAsync();
        }

    }
}
