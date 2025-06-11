using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var categories = await _context.Categories.Include(c => c.CategoryType).ToListAsync();
            return _mapper.Map<List<CategoryVM>>(categories);
        }

        public async Task<CategoryDetailVM> GetByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.CategoryType)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return null;

            var vm = _mapper.Map<CategoryDetailVM>(category);
            return vm;
        }

        public async Task<Category?> GetEntityByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<SelectListItem>> GetSelectListAsync()
        {
            return await _context.Categories
              .Select(c => new SelectListItem
              {
                  Value = c.Id.ToString(),
                  Text = c.Name
              })
              .ToListAsync();
        }
        public async Task CreateAsync(CategoryCreateVM vm)
        {
            var category = _mapper.Map<Models.Category>(vm);

            if (vm.ImageFile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string folderPath = Path.Combine(_env.WebRootPath, "uploads", "category");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string path = Path.Combine(folderPath, fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                category.Image = fileName;
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryEditVM vm)
        {
            var category = await _context.Categories.FindAsync(vm.Id);
            if (category == null) return;

            category.Name = vm.Name;
            category.CategoryTypeId = vm.CategoryTypeId;

            if (vm.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(category.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "uploads", "category", category.Image);
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string folderPath = Path.Combine(_env.WebRootPath, "uploads", "category");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string path = Path.Combine(folderPath, fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                category.Image = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return;

            if (!string.IsNullOrEmpty(category.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "uploads", "category", category.Image);
                if (File.Exists(path)) File.Delete(path);
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        public async Task<Dictionary<int, string>> GetCategoryTypesWithIdAsync()
        {
            return await _context.Categories
                .Include(c => c.CategoryType)
                .ToDictionaryAsync(c => c.Id, c => c.CategoryType.Name);
        }
    }
}