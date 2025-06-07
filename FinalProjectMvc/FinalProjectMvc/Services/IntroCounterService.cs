using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class IntroCounterService :IIntroCounterService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public IntroCounterService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<IntroCounter>> GetAllAsync()
        {
            return await _context.IntroCounters.Include(x => x.IntroVideo).ToListAsync();
        }

        public async Task<IntroCounter> GetByIdAsync(int id)
        {
            return await _context.IntroCounters.Include(x => x.IntroVideo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(IntroCounter counter)
        {
            await _context.IntroCounters.AddAsync(counter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IntroCounter counter)
        {
            _context.IntroCounters.Update(counter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var counter = await _context.IntroCounters.FindAsync(id);
            if (counter != null)
            {
                _context.IntroCounters.Remove(counter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.IntroCounters.AnyAsync(x => x.Id == id);
        }
    }
}
