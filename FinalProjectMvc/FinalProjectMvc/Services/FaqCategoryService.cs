using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.FaqCategory;
using FinalProjectMvc.ViewModels.Admin.FaqItem;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class FaqCategoryService : IFaqCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FaqCategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FaqCategoryDetailVM>> GetAllWithItemsAsync()
        {
            var categories = await _context.FaqCategories
                .Include(c => c.FaqItems)
                .Where(c => c.FaqItems.Any()) 
                .ToListAsync();

            return categories.Select(c => new FaqCategoryDetailVM
            {
                Id = c.Id,
                Title = c.Title,
                FaqItems = c.FaqItems.Select(i => new FaqItemVM
                {
                    Question = i.Question,
                    Answer = i.Answer
                }).ToList()
            }).ToList();
        }
        public async Task<List<FaqCategoryVM>> GetAllAsync()
        {
            var categories = await _context.FaqCategories.ToListAsync();
            return _mapper.Map<List<FaqCategoryVM>>(categories);
        }

        public async Task<FaqCategoryDetailVM> GetByIdAsync(int id)
        {
            var category = await _context.FaqCategories
                .Include(fc => fc.FaqItems)
                .FirstOrDefaultAsync(fc => fc.Id == id);

            if (category == null) throw new KeyNotFoundException("Category not found");
            return _mapper.Map<FaqCategoryDetailVM>(category);
        }

        public async Task CreateAsync(FaqCategoryCreateVM vm)
        {
            bool exists = await _context.FaqCategories.AnyAsync(c => c.Title.ToLower() == vm.Title.ToLower());
            if (exists) throw new InvalidOperationException("A category with the same title already exists.");

            var entity = _mapper.Map<FaqCategory>(vm);
            await _context.FaqCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<FaqCategoryEditVM> GetEditVMAsync(int id)
        {
            var category = await _context.FaqCategories.FindAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found");
            return _mapper.Map<FaqCategoryEditVM>(category);
        }

        public async Task UpdateAsync(FaqCategoryEditVM vm)
        {
            var category = await _context.FaqCategories.FindAsync(vm.Id);
            if (category == null) throw new KeyNotFoundException("Category not found");

            bool exists = await _context.FaqCategories
                .AnyAsync(c => c.Id != vm.Id && c.Title.ToLower() == vm.Title.ToLower());
            if (exists) throw new InvalidOperationException("A category with the same title already exists.");

            _mapper.Map(vm, category);
            _context.FaqCategories.Update(category);
            await _context.SaveChangesAsync();
        }
    
        public async Task DeleteAsync(int id)
        {
            var category = await _context.FaqCategories.FindAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found");

            _context.FaqCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
