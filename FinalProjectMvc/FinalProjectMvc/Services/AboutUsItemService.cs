using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutUsItem;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class AboutUsItemService : IAboutUsItemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AboutUsItemService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<AboutUsItemVM>> GetAllAsync()
        {
            var items = await _context.AboutUsItems.ToListAsync();
            return _mapper.Map<List<AboutUsItemVM>>(items);
        }

        public async Task<AboutUsItemDetailVM> GetDetailAsync(int id)
        {
            var item = await _context.AboutUsItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Item not found");
            return _mapper.Map<AboutUsItemDetailVM>(item);
        }

        public async Task CreateAsync(AboutUsItemCreateVM vm)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "icons");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Guid.NewGuid() + Path.GetExtension(vm.IconFile.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.IconFile.CopyToAsync(stream);
            }

            var aboutUs = await _context.AboutUses.FirstOrDefaultAsync();
            if (aboutUs == null) throw new KeyNotFoundException("No AboutUs exists");

            var aboutUsItem = new AboutUsItem
            {
                Title = vm.Title,
                Description = vm.Description,
                IconUrl = $"uploads/icons/{fileName}",
                AboutUsId = aboutUs.Id
            };

            await _context.AboutUsItems.AddAsync(aboutUsItem);
            await _context.SaveChangesAsync();
        }

        public async Task<AboutUsItemEditVM> GetEditAsync(int id)
        {
            var item = await _context.AboutUsItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Item not found");
            return _mapper.Map<AboutUsItemEditVM>(item);
        }

        public async Task EditAsync(AboutUsItemEditVM vm)
        {
            var item = await _context.AboutUsItems.FindAsync(vm.Id);
            if (item == null) throw new KeyNotFoundException("Item not found");

            item.Title = vm.Title;
            item.Description = vm.Description;

            if (vm.IconFile != null)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "uploads", "icons");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.IconFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.IconFile.CopyToAsync(stream);
                }

                item.IconUrl = $"uploads/icons/{fileName}";
            }

            _context.AboutUsItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.AboutUsItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Item not found");

            _context.AboutUsItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<AboutUsItem> GetByIdAsync(int id)
        {
            var item = await _context.AboutUsItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Item not found");
            return item;
        }
    }
}
