using AutoMapper.QueryableExtensions;
using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Table;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class TableService : ITableService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TableService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TableVM>> GetAllAsync()
        {
            return await _context.Tables
                .ProjectTo<TableVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TableDetailVM> GetByIdAsync(int id)
        {
            var table = await _context.Tables
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (table == null) return null;

            return _mapper.Map<TableDetailVM>(table);
        }

        public async Task CreateAsync(TableCreateVM vm)
        {
            var table = _mapper.Map<Table>(vm);
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }
    }
}
    

