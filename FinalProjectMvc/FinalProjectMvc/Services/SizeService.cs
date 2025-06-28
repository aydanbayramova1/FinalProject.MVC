using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Size;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class SizeService :ISizeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SizeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SizeVM>> GetAllAsync()
        {
            var sizes = await _context.Sizes.ToListAsync();
            return _mapper.Map<List<SizeVM>>(sizes);
        }

        public async Task CreateAsync(SizeCreateVM vm)
        {
            var size = _mapper.Map<Size>(vm);
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
        }
        public async Task<List<SelectListItem>> GetSelectListAsync()
        {
            return await _context.Sizes
              .Select(c => new SelectListItem
              {
                  Value = c.Id.ToString(),
                  Text = c.Name
              })
              .ToListAsync();
        }
    }
}
