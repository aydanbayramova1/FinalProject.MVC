using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.CategoryType;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class CategoryTypeService : ICategoryTypeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryTypeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryTypeVM>> GetAllAsync()
        {
            var types = await _context.CategoryTypes.ToListAsync();
            return _mapper.Map<List<CategoryTypeVM>>(types);
        }

        public async Task CreateAsync(CategoryTypeCreateVM vm)
        {
            var entity = _mapper.Map<CategoryType>(vm);
            await _context.CategoryTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
    
}
